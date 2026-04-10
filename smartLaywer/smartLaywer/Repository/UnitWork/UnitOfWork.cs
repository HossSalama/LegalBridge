using System.Collections;

namespace ExaminationSystem_API.Repository.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LegalManagementContext _context;
        private Hashtable _repositories;
        public IFinancialRepository Financials { get; private set; }
        public IGenericRepository<ActualPayment> ActualPayments { get; private set; }
        public IGenericRepository<PaymentSchedule> Schedules { get; private set; }
        public IGenericRepository<AdminExpense> Expenses { get; private set; }

        public UnitOfWork(LegalManagementContext context)
        {
            _context = context;
            Financials = new FinancialRepository(_context);
            ActualPayments = new GenericRepository<ActualPayment>(_context);
            Expenses = new GenericRepository<AdminExpense>(_context);

        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose(){  }
    }
}
