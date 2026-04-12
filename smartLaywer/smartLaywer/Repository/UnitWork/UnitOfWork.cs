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

        // Cases 
        public ICaseRepository Cases { get; private set; }
        public IGenericRepository<Client> Clients { get; private set; }
        public IGenericRepository<CaseType> CaseTypes { get; private set; }
        public IGenericRepository<CaseStatus> CaseStatuses { get; private set; }
        public IGenericRepository<Court> Courts { get; private set; }
        public IGenericRepository<User> Users { get; private set; }

        // Client Domain
        public IClientRepository Client { get; private set; }

        public UnitOfWork(LegalManagementContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Financials = new FinancialRepository(_context , _mapper);
            ActualPayments = new GenericRepository<ActualPayment>(_context);
            Expenses = new GenericRepository<AdminExpense>(_context);
            Schedules = new GenericRepository<PaymentSchedule>(_context);
            Hearing = new HearingRepository(_context , _mapper);

            // Cases
            Cases = new CaseRepository(_context);
            Clients = new GenericRepository<Client>(_context);
            CaseTypes = new GenericRepository<CaseType>(_context);
            CaseStatuses = new GenericRepository<CaseStatus>(_context);
            Courts = new GenericRepository<Court>(_context);
            Users = new GenericRepository<User>(_context);
            Client = new ClientRepository(context);

        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() { }
    }
}