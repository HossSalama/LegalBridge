namespace smartLaywer.Repository.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFinancialRepository Financials { get; }
        IGenericRepository<ActualPayment> ActualPayments { get; }
        IGenericRepository<PaymentSchedule> Schedules { get; }
        IGenericRepository<AdminExpense> Expenses { get; }
        IHearingRepository Hearing { get; }

        // Cases domain
        ICaseRepository Cases { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<CaseType> CaseTypes { get; }
        IGenericRepository<CaseStatus> CaseStatuses { get; }
        IGenericRepository<Court> Courts { get; }
        IGenericRepository<User> Users { get; }

        // Client Doman
        IClientRepository Client { get; }
        Task<int> CompleteAsync();
    }
}