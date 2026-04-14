

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

            if (feeData == null) throw new KeyNotFoundException("”Ã· «·√ ⁄«» €Ì— „ÊÃÊœ.");

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
                ClientName = fees.FirstOrDefault()?.Client?.FullName ?? "⁄„Ì· „Õœœ",
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
                    Description = $"œ›⁄… „«·Ì… - ≈Ì’«· —ﬁ„ {p.ReceiptNumber}",
                    Amount = p.Amount,
                    Type = "Credit"
                }));

                var expenses = await _unitOfWork.Expenses.GetAllQueryableNoTracking()
                    .Where(e => e.CaseId == fee.CaseId)
                    .ToListAsync();

                caseDto.Transactions.AddRange(expenses.Select(e => new FinancialTransactionDto
                {
                    Date = e.ExpenseDate,
                    Description = $"„’«—Ì› ≈œ«—Ì…: {e.Description}",
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
        private async Task<decimal> GetTotalOverdueForClientAsync(int clientId)
        {
            return await _unitOfWork.Schedules.GetAllQueryableNoTracking()
                .Where(ps => ps.Fee.ClientId == clientId &&
                             ps.DueDate < DateTime.Now &&
                             ps.Status != PaymentStatusEnum.Paid)
                .SumAsync(ps => ps.PlannedAmount);
        }

       
           





    }
}
