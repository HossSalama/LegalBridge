namespace smartLaywer.Repository.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFinancialRepository Financials { get; }
        IGenericRepository<ActualPayment> ActualPayments { get; }
        IGenericRepository<PaymentSchedule> Schedules { get; }
        IGenericRepository<AdminExpense> Expenses { get; }
        IHearingRepository Hearing { get; }

        ICaseRepository Cases { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<CaseType> CaseTypes { get; }
        IGenericRepository<CaseStatus> CaseStatuses { get; }
        IGenericRepository<Court> Courts { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Report> Reports { get; }
        IClientRepository Client { get; }
        IGenericRepository<LegalLibrary> LegalLibraries { get; }
        Task<int> CompleteAsync();
    }
}