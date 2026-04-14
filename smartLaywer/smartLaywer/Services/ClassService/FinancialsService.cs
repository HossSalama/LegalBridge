

using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using System.Security.Claims;

namespace smartLaywer.Service.ClassService
{
    public class FinancialsService : IFinancialsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
        private readonly AuthenticationStateProvider _authStateProvider;

        public FinancialsService(IUnitOfWork unitOfWork, IMapper mapper , AuthenticationStateProvider authenticationStateProvider )
        {
            _unitOfWork = unitOfWork;
            _mapper=mapper;
            _authStateProvider = authenticationStateProvider;
        }

        public async Task<ScheduleValidationResult> AddInstallmentToScheduleAsync(PaymentSchedule newInstallment)
        {
            var feeData = await _unitOfWork.Financials.GetAllQueryableNoTracking()
                .Where(f => f.Id == newInstallment.FeeId)
                .Select(f => new
                {
                    Total = f.TotalAmount,
                    ScheduledSum = f.PaymentSchedules.Sum(ps => ps.PlannedAmount)
                })
                .FirstOrDefaultAsync();

            if (feeData == null) throw new KeyNotFoundException("ÓÌá ÇáÃÊÚÇÈ ÛíÑ ãæÌæÏ.");

            var totalAfterAddition = feeData.ScheduledSum + newInstallment.PlannedAmount;

            var result = new ScheduleValidationResult
            {
                CaseTotalFee = feeData.Total,
                AlreadyScheduled = feeData.ScheduledSum,
                RemainingToSchedule = feeData.Total - feeData.ScheduledSum
            };

            if (totalAfterAddition > feeData.Total)
            {
                result.CanAdd = false;
                result.Status = "Excess";
                return result;
            }

            try
            {
                newInstallment.Status = PaymentStatusEnum.Pending;

                var lastNumber = await _unitOfWork.Schedules.GetAllQueryableNoTracking()
                    .Where(ps => ps.FeeId == newInstallment.FeeId)
                    .MaxAsync(ps => (int?)ps.InstallmentNumber) ?? 0;

                newInstallment.InstallmentNumber = lastNumber + 1;

                await _unitOfWork.Schedules.AddAsync(newInstallment);
                await _unitOfWork.CompleteAsync();

                result.CanAdd = true;
                result.Status = (totalAfterAddition == feeData.Total) ? "Equal" : "Remaining";
            }
            catch (Exception)
            {
                result.CanAdd = false;
                result.Status = "Error";
            }

            return result;
        }


        public async Task<bool> SaveSchedulesAsync(CreateSchedulesDto dto)
        {
            if (dto == null || !dto.Schedules.Any()) return false;

            try
            {
                var feeData = await _unitOfWork.Financials.GetAllQueryableNoTracking()
                    .Where(f => f.Id == dto.FeeId)
                    .Select(f => new {
                        Total = f.TotalAmount,
                        CurrentSum = f.PaymentSchedules.Sum(ps => ps.PlannedAmount)
                    }).FirstOrDefaultAsync();

                if (feeData == null) return false;

                var newTotal = dto.Schedules.Sum(s => s.Amount);
                if ((feeData.CurrentSum + newTotal) > feeData.Total) return false;

                var lastNumber = await _unitOfWork.Schedules.GetAllQueryableNoTracking()
                    .Where(ps => ps.FeeId == dto.FeeId)
                    .MaxAsync(ps => (int?)ps.InstallmentNumber) ?? 0;
                var entities = dto.Schedules.Select((s, index) => new PaymentSchedule
                {
                    FeeId = dto.FeeId,
                    PlannedAmount = s.Amount,
                    DueDate = s.DueDate,
                    InstallmentNumber = lastNumber + (index + 1),
                    Status = PaymentStatusEnum.Pending
                }).ToList();

                await _unitOfWork.Schedules.AddRangeAsync(entities);
                return await _unitOfWork.CompleteAsync() > 0;
            }
            catch { return false; }
        }

        public async Task<FinancialStatDto> GetDashboardStatsAsync() =>
    await _unitOfWork.Financials.GetFinancialSummaryAsync();


        public async Task<PaginatedList<FeeDetailsDto>> GetPagedFeesAsync(string? searchTerm, int pageNumber) =>
            await _unitOfWork.Financials.GetPagedFeesAsync(searchTerm, pageNumber, PageSize);


        public async Task<bool> CollectPaymentAsync(int feeId, decimal amount, PaymentMethodEnum method, int currentUserId)
        {
            if (amount <= 0) return false;

            try
            {
                var payment = new ActualPayment
                {
                    FeeId = feeId,
                    Amount = amount,
                    PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                    Method = method,
                    ReceivedBy = currentUserId, 
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.ActualPayments.AddAsync(payment);

                var pendingSchedules = await _unitOfWork.Schedules.GetAllQueryableTracking()
                    .Where(ps => ps.FeeId == feeId && ps.Status != PaymentStatusEnum.Paid)
                    .OrderBy(ps => ps.DueDate)
                    .ToListAsync();

                decimal remainingToDistribute = amount;

                foreach (var schedule in pendingSchedules)
                {
                    if (remainingToDistribute <= 0) break;
                    decimal amountNeededForThisSchedule = schedule.PlannedAmount;

                    if (remainingToDistribute >= amountNeededForThisSchedule)
                    {
                        schedule.Status = PaymentStatusEnum.Paid;
                        remainingToDistribute -= amountNeededForThisSchedule;
                    }
                    else
                    {

                         schedule.PlannedAmount -= remainingToDistribute;

                        remainingToDistribute = 0; 
                        break;
                    }
                }

                return await _unitOfWork.CompleteAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CollectPayment: {ex.Message}");
                return false;
            }
        }











        /// <summary>
        /// ÊÓÌíá ÚãáíÉ ÏİÚ İÚáíÉ ÌÏíÏÉ¡ æÊæáíÏ ÑŞã ÅíÕÇá áåÇ¡ Ëã ÅÚÇÏÉ ÊÓæíÉ ÌÏæá ÇáÃŞÓÇØ ÈäÇÁğ Úáì ÇáãÈáÛ ÇáÌÏíÏ.
        /// </summary>
        /// <param name="dto">ÈíÇäÇÊ ÇáÏİÚÉ (ÇáãÈáÛ¡ ÇáÊÇÑíÎ¡ ÑŞã ÇáŞÖíÉ¡ ÅáÎ)</param>
        public async Task<bool> RegisterPaymentAsync(PaymentCreationDto dto)
        {
     
            dto.ReceiptNumber = await GenerateNextReceiptNumberAsync();
            var payment = _mapper.Map<ActualPayment>(dto);
            await _unitOfWork.ActualPayments.AddAsync(payment);
            await ReconcilePaymentSchedulesAsync(dto.FeeId);
            return true; 
        }

        /// <summary>
        /// ãÍÑß ÇáÊÓæíÉ ÇáãÇáí: íŞæã ÈãŞÇÑäÉ ÅÌãÇáí ÇáãÏİæÚÇÊ ãÚ ÌÏæá ÇáÃŞÓÇØ æÊÍÏíË ÍÇáÉ ßá ŞÓØ ÊÇÑíÎíÇğ (ãä ÇáÃŞÏã ááÃÍÏË).
        /// </summary>
        /// <param name="feeId">ãÚÑİ ÇáÃÊÚÇÈ ÇáÎÇÕ ÈÇáŞÖíÉ</param>
        public async Task<bool> ReconcilePaymentSchedulesAsync(int feeId)
        {
            var schedules = await _unitOfWork.Schedules.GetAllQueryableTracking()
                .Where(ps => ps.FeeId == feeId)
                .ToListAsync();

            var actualPayments = await _unitOfWork.ActualPayments.GetAllQueryableTracking()
                .Where(ap => ap.FeeId == feeId)
                .ToListAsync();

            decimal totalPaidAmount = actualPayments.Sum(ap => ap.Amount);

            foreach (var schedule in schedules.OrderBy(s => s.DueDate))
            {
                if (totalPaidAmount >= schedule.PlannedAmount)
                {
                    schedule.Status = PaymentStatusEnum.Paid;
                    totalPaidAmount -= schedule.PlannedAmount;
                }
                else if (totalPaidAmount > 0)
                {
                    schedule.Status = PaymentStatusEnum.Partial;
                    totalPaidAmount = 0;
                }
                else
                {
                    schedule.Status = PaymentStatusEnum.Unpaid;
                }
            }

            return await _unitOfWork.CompleteAsync() > 0;
        }

        /// <summary>
        /// ÊæŞÚ ÇáÏÎá ÇáãÇáí áİÊÑÉ ãÍÏÏÉ ÈäÇÁğ Úáì ÇáÃŞÓÇØ ÇáãÌÏæáÉ ÇáÊí áã ÊõÏİÚ ÈÚÏ.
        /// </summary>
        /// <param name="month">ÇáÔåÑ ÇáãØáæÈ</param>
        /// <param name="year">ÇáÓäÉ ÇáãØáæÈÉ</param>
        public async Task<decimal> GetExpectedIncomeAsync(int month, int year)
        {
            return await _unitOfWork.Schedules.GetAllQueryableNoTracking()
                .Where(ps => ps.DueDate.Month == month && ps.DueDate.Year == year && ps.Status != PaymentStatusEnum.Paid)
                .SumAsync(ps => ps.PlannedAmount);
        }

        /// <summary>
        /// ÍĞİ ÏİÚÉ ãÇáíÉ ãÓÌáÉ ãÓÈŞÇğ¡ æÅÚÇÏÉ ÊÓæíÉ ÇáÃŞÓÇØ áÊÚæÏ áÍÇáÊåÇ ÇáÃÕáíÉ ŞÈá Êáß ÇáÏİÚÉ.
        /// </summary>
        /// <param name="paymentId">ãÚÑİ ÇáÏİÚÉ ÇáãÑÇÏ ÍĞİåÇ</param>
        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            var payment = await _unitOfWork.ActualPayments.GetByIdAsync(paymentId);
            if (payment == null) return false;

            int feeId = payment.FeeId;
            _unitOfWork.ActualPayments.Delete(payment.Id);
            await ReconcilePaymentSchedulesAsync(feeId);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        /// <summary>
        /// ÍÓÇÈ ÅÌãÇáí ÇáãÈÇáÛ ÇáãÊÃÎÑÉ Úáì ãÓÊæì ÇáäÙÇã ÈÇáßÇãá (ÇáÊí ÊÌÇæÒÊ ÊÇÑíÎ ÇÓÊÍŞÇŞåÇ æáã ÊõÏİÚ).
        /// </summary>
        public async Task<decimal> GetTotalOverdueAmountAsync()
        {
            var today = DateTime.Today;

            var overdueAmount = await _unitOfWork.Schedules.GetAllQueryableTracking()
                .Where(ps => ps.DueDate < today && ps.Status != PaymentStatusEnum.Paid)
                .SumAsync(ps => ps.PlannedAmount);

            return overdueAmount;
        }

        /// <summary>
        /// ÊæáíÏ ÑŞã ÅíÕÇá ãÊÓáÓá æÊáŞÇÆí ÈäÇÁğ Úáì ÇáÓäÉ ÇáÍÇáíÉ (ãËÇá: REC-2026-0001).
        /// </summary>
        public async Task<string> GenerateNextReceiptNumberAsync()
        {
            var year = DateTime.Now.Year.ToString();
            var prefix = $"REC-{year}-";

            var lastReceipt = await _unitOfWork.ActualPayments.GetAllQueryableTracking()
                .Where(p => p.ReceiptNumber != null && p.ReceiptNumber.StartsWith(prefix))
                .OrderByDescending(p => p.ReceiptNumber)
                .Select(p => p.ReceiptNumber)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(lastReceipt))
            {
                return $"{prefix}0001";
            }

            var lastNumberPart = lastReceipt.Replace(prefix, "");
            if (int.TryParse(lastNumberPart, out int lastNumber))
            {
                return $"{prefix}{(lastNumber + 1).ToString("D4")}"; 
            }

            return $"{prefix}{Guid.NewGuid().ToString().Substring(0, 4)}"; 
        }

        /// <summary>
        /// ßÔİ ÇáÍÓÇÈ ÇáÔÇãá ááÚãíá: íÌãÚ ßá ÇáŞÖÇíÇ¡ ÇáÍÑßÇÊ ÇáãÇáíÉ (ãÏİæÚÇÊ/ãÕÇÑíİ)¡ æÌÏÇæá ÇáÃŞÓÇØ İí ãßÇä æÇÍÏ.
        /// </summary>
        /// <param name="clientId">ãÚÑİ ÇáÚãíá</param>
        public async Task<ClientFinancialProfileDto> GetClientFullFinancialHistoryAsync(int clientId)
        {
            var fees = await _unitOfWork.Financials.GetAllQueryableNoTracking()
                .Where(f => f.ClientId == clientId)
                .Include(f => f.Client)
                .Include(f => f.Case)
                .Include(f => f.ActualPayments)
                .Include(f => f.PaymentSchedules)
                .ToListAsync();

            var profile = new ClientFinancialProfileDto
            {
                ClientId = clientId,
                ClientName = fees.FirstOrDefault()?.Client?.FullName ?? "Úãíá ãÍÏÏ", 
                TotalAgreedAmount = fees.Sum(f => f.TotalAmount),
                TotalPaid = fees.SelectMany(f => f.ActualPayments).Sum(p => p.Amount),
                TotalOverdue = await GetTotalOverdueForClientAsync(clientId) 
            };

            foreach (var fee in fees)
            {
                var caseDto = new CaseFinanceDto
                {
                    CaseId = fee.CaseId,
                    CaseNumber = fee.Case.CaseNumber,
                    CaseTotalFee = fee.TotalAmount
                };

                caseDto.Transactions.AddRange(fee.ActualPayments.Select(p => new FinancialTransactionDto
                {
                    Date = p.CreatedAt,
                    Description = $"ÏİÚÉ ãÇáíÉ - ÅíÕÇá ÑŞã {p.ReceiptNumber}",
                    Amount = p.Amount,
                    Type = "Credit"
                }));

                var expenses = await _unitOfWork.Expenses.GetAllQueryableNoTracking()
                    .Where(e => e.CaseId == fee.CaseId)
                    .ToListAsync();

                caseDto.Transactions.AddRange(expenses.Select(e => new FinancialTransactionDto
                {
                    Date = e.ExpenseDate,
                    Description = $"ãÕÇÑíİ ÅÏÇÑíÉ: {e.Description}",
                    Amount = e.Amount,
                    Type = "Debit"
                }));

                caseDto.Installments = _mapper.Map<List<InstallmentDetailDto>>(fee.PaymentSchedules)
                    .OrderBy(i => i.DueDate)
                    .ToList();

                profile.Cases.Add(caseDto);
            }

            return profile;
        }

        /// <summary>
        /// ãíËæÏ ãÓÇÚÏÉ (Private) áÍÓÇÈ ÅÌãÇáí ÇáãÈÇáÛ ÇáãÊÃÎÑÉ áÚãíá ãÍÏÏ İŞØ.
        /// </summary>
        private async Task<decimal> GetTotalOverdueForClientAsync(int clientId)
        {
            return await _unitOfWork.Schedules.GetAllQueryableNoTracking()
                .Where(ps => ps.Fee.ClientId == clientId &&
                             ps.DueDate < DateTime.Now &&
                             ps.Status != PaymentStatusEnum.Paid)
                .SumAsync(ps => ps.PlannedAmount);
        }

        public async Task<List<RevenueSummaryDto>> GetUpcomingRevenueAsync()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(4);

            var revenues = await _unitOfWork.Schedules.GetAllQueryableNoTracking()
                .Where(t => t.DueDate >= startDate && t.DueDate < endDate&& t.Status != PaymentStatusEnum.Paid)
                .GroupBy(t => new { t.DueDate.Year, t.DueDate.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new RevenueSummaryDto
                {
                    MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM", new CultureInfo("ar-EG")),
                    TotalValue = g.Sum(t => t.PlannedAmount),
                })
                .ToListAsync();

            return revenues;
        }

        /// <summary>
        /// ÅÖÇİÉ ÌÏæá ÃŞÓÇØ ÌÏíÏ áŞÖíÉ ãÚíäÉ.
        /// </summary>
        public async Task<bool> CreatePaymentSchedulesAsync(int feeId, List<InstallmentCreationDto> newSchedules)
        {
            if (newSchedules == null || !newSchedules.Any()) return false;

            foreach (var dto in newSchedules)
            {
                var schedule = new PaymentSchedule
                {
                    FeeId = feeId,
                    PlannedAmount = dto.Amount,
                    DueDate = dto.DueDate,
                    Status = PaymentStatusEnum.Unpaid, // ÇáÍÇáÉ ÇáÇİÊÑÇÖíÉ
                };
                await _unitOfWork.Schedules.AddAsync(schedule);
            }

            // ÈÚÏ ÇáÅÖÇİÉ¡ ÈäÇÏí ãíËæÏ ÇáÊÓæíÉ ÚÔÇä áæ İíå ãÈÇáÛ ãÏİæÚÉ ÒíÇÏÉ ÊÊæÒÚ Ú ÇáÃŞÓÇØ ÇáÌÏíÏÉ
            await ReconcilePaymentSchedulesAsync(feeId);

            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
