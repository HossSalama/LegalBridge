namespace smartLaywer.Repository.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFinancialRepository Financials { get; }
        IGenericRepository<ActualPayment> ActualPayments { get; }
        IGenericRepository<PaymentSchedule> Schedules { get; }
        IGenericRepository<AdminExpense> Expenses { get; }
        IHearingRepository Hearing { get; }
        IInvestigationRepository Investigations { get; }

        ICaseRepository Cases { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<CaseType> CaseTypes { get; }
        IGenericRepository<CaseStatus> CaseStatuses { get; }
        IGenericRepository<Court> Courts { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Report> Reports { get; }
        IClientRepository Client { get; }
        IGenericRepository<Document> Documents { get; }

        IHearingDetailsRepository HearingDetails { get; }

        IGenericRepository<smartLaywer.Models.LegalLibrary> LegalLibraries { get; }
        Task<int> CompleteAsync();
        IDocumentRepository Document { get; }
        Task<int> SaveChangesAsync();
    }
}