using smartLaywer.DTO.Cases;
using smartLaywer.Repository.UnitWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using smartLaywer.Models;
using smartLaywer.Enum;

namespace smartLaywer.Services.ClassService
{
    public class CaseService : ICaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CaseSummaryDto>> GetAllCasesAsync(string? searchTerm = null)
        {
            var query = _unitOfWork.Cases
                .GetAllQueryableNoTracking()
                .Include(c => c.Client)
                .Include(c => c.CaseType)
                .Include(c => c.Status)
                .Include(c => c.Hearings)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c =>
                    c.Title.Contains(searchTerm) ||
                    (c.CaseNumber != null && c.CaseNumber.Contains(searchTerm)) ||
                    c.Client.FullName.Contains(searchTerm));
            }

            var cases = await query
                .OrderByDescending(c => c.OpenDate)
                .ToListAsync();

            return _mapper.Map<List<CaseSummaryDto>>(cases);
        }

        public async Task<Case?> GetCaseWithDetailsAsync(int id)
            => await _unitOfWork.Cases.GetCaseWithDetailsAsync(id);

        public async Task<CaseStatsDto> GetCaseStatsAsync()
            => await _unitOfWork.Cases.GetCaseStatsAsync();

        public async Task ValidateClientIdentifierUniquenessAsync(Client client)
        {
            if (client.ClientType == ClientTypeEnum.Individual)
            {
                if (string.IsNullOrWhiteSpace(client.NationalId))
                    throw new InvalidOperationException("ÇáÑÞã ÇáÞæãí ãØáæÈ ááÃÝÑÇÏ.");

                var duplicate = await _unitOfWork.Clients
                    .GetAllQueryableNoTracking()
                    .AnyAsync(c => c.NationalId == client.NationalId.Trim());

                if (duplicate)
                    throw new InvalidOperationException($"ÇáÑÞã ÇáÞæãí '{client.NationalId}' ãÓÌá ãÓÈÞÇð áÚãíá ÂÎÑ.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(client.CommercialReg))
                    throw new InvalidOperationException("ÑÞã ÇáÊÓÌíá ÇáÊÌÇÑí ãØáæÈ ááÔÑßÇÊ æÇáÌåÇÊ ÇáÍßæãíÉ.");

                var duplicate = await _unitOfWork.Clients
                    .GetAllQueryableNoTracking()
                    .AnyAsync(c => c.CommercialReg == client.CommercialReg.Trim());

                if (duplicate)
                    throw new InvalidOperationException($"ÑÞã ÇáÊÓÌíá '{client.CommercialReg}' ãÓÌá ãÓÈÞÇð áÚãíá ÂÎÑ.");
            }
        }

        public async Task AddCaseAsync(Case caseEntity)
        {
            if (caseEntity.ClientId == 0) throw new InvalidOperationException("ClientId is required.");
            if (caseEntity.CaseTypeId == 0) throw new InvalidOperationException("CaseTypeId is required.");
            if (caseEntity.CourtId == 0) throw new InvalidOperationException("CourtId is required.");
            if (caseEntity.StatusId == 0) throw new InvalidOperationException("StatusId is required.");
            if (caseEntity.AssignedLawyerId == 0) throw new InvalidOperationException("AssignedLawyerId is required.");

            caseEntity.UpdatedAt = DateTime.Now;
            await _unitOfWork.Cases.AddAsync(caseEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddCaseAsync(Case newCase, Fee? fee = null)
        {
            newCase.UpdatedAt = DateTime.Now;
            await _unitOfWork.Cases.AddAsync(newCase);
            await _unitOfWork.CompleteAsync();

            if (fee != null && fee.TotalAmount > 0)
            {
                fee.CaseId = newCase.Id;
                fee.ClientId = newCase.ClientId;
                fee.CreatedAt = DateTime.Now;
                await _unitOfWork.Financials.AddFeeAsync(fee);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task UpdateCaseAsync(CaseEditDto dto)
        {
            var existing = await _unitOfWork.Cases.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Case {dto.Id} not found.");

            existing.StatusId = dto.StatusId;
            existing.CourtId = dto.CourtId;
            existing.DeptId = dto.DeptId;
            existing.AssignedLawyerId = dto.AssignedLawyerId;
            existing.Title = dto.Title;
            existing.Stage = dto.Stage;
            existing.CloseDate = dto.CloseDate;
            existing.ArchiveNote = dto.ArchiveNote;
            existing.UpdatedAt = DateTime.Now;

            _unitOfWork.Cases.Update(existing);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCaseAsync(int id)
        {
            await _unitOfWork.Cases.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CaseViewDto>> GetAllCasesForDropdownAsync()
        {
            return await _unitOfWork.Cases
                .GetAllQueryableNoTracking()
                .Where(c => c.StatusId == (int)CaseStatusEnum.Open)
                .Select(c => new CaseViewDto
                {
                    Id = c.Id,
                    CaseNumber = c.CaseNumber,
                    ClientName = c.Client.FullName
                })
                .ToListAsync();
        }
    }
}