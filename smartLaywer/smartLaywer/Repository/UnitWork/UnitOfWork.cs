using smartLaywer.Components.Pages;
using smartLaywer.Repository.UnitWork;

namespace smartLaywer.Repository.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LegalManagementContext _context;
        private readonly IMapper _mapper;

        public IFinancialRepository Financials { get; private set; }
        public IGenericRepository<ActualPayment> ActualPayments { get; private set; }
        public IGenericRepository<PaymentSchedule> Schedules { get; private set; }
        public IGenericRepository<AdminExpense> Expenses { get; private set; }
        public IHearingRepository Hearing { get; private set; }
        public ICaseRepository Cases { get; private set; }
        public IClientRepository Client { get; private set; }
        public IGenericRepository<Client> Clients { get; private set; }
        public IGenericRepository<CaseType> CaseTypes { get; private set; }
        public IGenericRepository<CaseStatus> CaseStatuses { get; private set; }
        public IGenericRepository<Court> Courts { get; private set; }
        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<Department> Departments { get; private set; }
        public IHearingDetailsRepository HearingDetails { get; private set; }
        public IInvestigationRepository Investigations { get; }
        public IDocumentRepository Document { get; }

        public IGenericRepository<Report> Reports { get; private set; }
        public IGenericRepository<LegalLibrary> LegalLibraries { get; private set; }
        public UnitOfWork(LegalManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            Financials = new FinancialRepository(_context, _mapper);
            ActualPayments = new GenericRepository<ActualPayment>(_context);
            Expenses = new GenericRepository<AdminExpense>(_context);
            Schedules = new GenericRepository<PaymentSchedule>(_context);
            Hearing = new HearingRepository(_context, _mapper);
            Cases = new CaseRepository(_context);
            Client = new ClientRepository(_context);
            Clients = new GenericRepository<Client>(_context);
            CaseTypes = new GenericRepository<CaseType>(_context);
            CaseStatuses = new GenericRepository<CaseStatus>(_context);
            Courts = new GenericRepository<Court>(_context);
            Users = new GenericRepository<User>(_context);
            Departments = new GenericRepository<Department>(_context);
            Reports = new GenericRepository<Report>(_context);
<<<<<<< HEAD
            HearingDetails = new HearingDetailsRepository(_context);
            Investigations = new InvestigationRepository(_context);
            Document = new DocumentRepository(context);

=======

            LegalLibraries = new GenericRepository<LegalLibrary>(_context);
>>>>>>> 2db0ea3599fed4764943d4eb5f9b239a765368dd
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() { }
    }
}