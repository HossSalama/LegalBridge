namespace smartLaywer.Repository.ClassRepository
{
    public class HearingRepository : GenericRepository<Hearing>, IHearingRepository
    {
        private readonly LegalManagementContext _context;
        public HearingRepository(LegalManagementContext context) : base(context)
        {
            _context = context;
        }

    }
}
