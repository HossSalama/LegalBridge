namespace ExaminationSystem_API.Repository.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFinancialRepository Financials { get; }
        IGenericRepository<ActualPayment> ActualPayments { get;}
         IGenericRepository<PaymentSchedule> Schedules { get; }
         IGenericRepository<AdminExpense> Expenses { get; }
        Task<int> CompleteAsync();
    }
}
