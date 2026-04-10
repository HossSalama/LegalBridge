namespace ExaminationSystem_API.Repository.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LegalManagementContext _context;
        public IFinancialRepository Financials { get; private set; }
        public IGenericRepository<ActualPayment> ActualPayments { get; private set; }
        public IGenericRepository<PaymentSchedule> Schedules { get; private set; }
        public IGenericRepository<AdminExpense> Expenses { get; private set; }
        public IHearingRepository Hearing { get; private set; }

        public UnitOfWork(LegalManagementContext context)
        {
            _context = context;
            Financials = new FinancialRepository(_context);
            ActualPayments = new GenericRepository<ActualPayment>(_context);
            Expenses = new GenericRepository<AdminExpense>(_context);
            Schedules = new GenericRepository<PaymentSchedule>(_context);
            Hearing = new HearingRepository(_context);

        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose(){  }
    }
}
