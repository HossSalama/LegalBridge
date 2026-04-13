using smartLaywer.Repository.UnitWork;

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

        /// <summary>
        /// Returns a flat list of CaseSummaryDto for the cases table.
        /// Optional search by case number, title, or client name.
        /// </summary>
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

        /// <summary>
        /// Returns a fully-loaded Case entity for the details view.
        /// Returns null if not found — caller must handle null.
        /// </summary>
        public async Task<Case?> GetCaseWithDetailsAsync(int id)
            => await _unitOfWork.Cases.GetCaseWithDetailsAsync(id);

        public async Task<CaseStatsDto> GetCaseStatsAsync()
            => await _unitOfWork.Cases.GetCaseStatsAsync();

        /// <summary>
        /// Inserts a new case.
        /// Validates all required FK fields are non-zero before hitting the DB.
        /// </summary>
        /// <summary>
        /// Validates that NationalId (for Individual) or CommercialReg (for others) is unique
        /// across all existing clients. Throws InvalidOperationException if a duplicate is found.
        /// Call this from ClientService.AddClientAsync before persisting the new client.
        /// </summary>
        public async Task ValidateClientIdentifierUniquenessAsync(Client client)
        {
            if (client.ClientType == ClientTypeEnum.Individual)
            {
                if (string.IsNullOrWhiteSpace(client.NationalId))
                    throw new InvalidOperationException("الرقم القومي مطلوب للأفراد.");

                var duplicate = await _unitOfWork.Clients
                    .GetAllQueryableNoTracking()
                    .AnyAsync(c => c.NationalId == client.NationalId.Trim());

                if (duplicate)
                    throw new InvalidOperationException($"الرقم القومي '{client.NationalId}' مسجل مسبقاً لعميل آخر.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(client.CommercialReg))
                    throw new InvalidOperationException("رقم التسجيل التجاري مطلوب للشركات والجهات الحكومية.");

                var duplicate = await _unitOfWork.Clients
                    .GetAllQueryableNoTracking()
                    .AnyAsync(c => c.CommercialReg == client.CommercialReg.Trim());

                if (duplicate)
                    throw new InvalidOperationException($"رقم التسجيل '{client.CommercialReg}' مسجل مسبقاً لعميل آخر.");
            }
        }

        public async Task AddCaseAsync(Case caseEntity)
        {
            // Guard: required FK fields must be set
            if (caseEntity.ClientId == 0)
                throw new InvalidOperationException("ClientId is required.");
            if (caseEntity.CaseTypeId == 0)
                throw new InvalidOperationException("CaseTypeId is required.");
            if (caseEntity.CourtId == 0)
                throw new InvalidOperationException("CourtId is required.");
            if (caseEntity.StatusId == 0)
                throw new InvalidOperationException("StatusId is required.");
            if (caseEntity.AssignedLawyerId == 0)
                throw new InvalidOperationException("AssignedLawyerId is required.");

            caseEntity.UpdatedAt = DateTime.Now;
            await _unitOfWork.Cases.AddAsync(caseEntity);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Updates ONLY the editable fields from CaseEditDto.
        /// Loads the tracked entity first so EF only updates what changed.
        /// Read-only fields (ClientId, CaseTypeId, OpenDate, CaseNumber) are never touched.
        /// </summary>
        public async Task UpdateCaseAsync(CaseEditDto dto)
        {
            // Load tracked entity — Update() will change only the fields we set
            var existing = await _unitOfWork.Cases.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Case {dto.Id} not found.");

            // Apply only editable fields
            existing.StatusId = dto.StatusId;
            existing.CourtId = dto.CourtId;
            existing.DeptId = dto.DeptId;
            existing.AssignedLawyerId = dto.AssignedLawyerId;
            existing.Title = dto.Title;
            existing.Stage = dto.Stage;
            existing.CloseDate = dto.CloseDate;
            existing.ArchiveNote = dto.ArchiveNote;
            existing.UpdatedAt = DateTime.Now;

            // GetByIdAsync uses FindAsync which returns a tracked entity,
            // so Update() just marks it Modified — no duplicate tracking issue.
            _unitOfWork.Cases.Update(existing);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCaseAsync(int id)
        {
            await _unitOfWork.Cases.Delete(id);   // ← was missing await
            await _unitOfWork.CompleteAsync();
        }
    }
}