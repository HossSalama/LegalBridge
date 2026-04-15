
using smartLaywer.Mapping.FinancialMapping;
using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.ClassService;
using smartLaywer.Services.ReportService;

namespace smartLaywer.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IFinancialRepository, FinancialRepository>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IHearingRepository, HearingRepository>();
            services.AddScoped<IHearingDetailsRepository, HearingDetailsRepository>();
            services.AddScoped<IInvestigationRepository, InvestigationRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFinancialsService, FinancialsService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IHearingService, HearingService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourtService, CourtService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDashboardService, DashboardService>();
<<<<<<< HEAD
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IHearingDetailsService, HearingDetailsService>();
            services.AddScoped<IInvestigationService, InvestigationService>();
            services.AddScoped<IDocumentService, DocumentService>();
=======
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<ILegalLibraryService, LegalLibraryService>();
>>>>>>> 2db0ea3599fed4764943d4eb5f9b239a765368dd

            return services;
        }
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FinancialProfile).Assembly);
            return services;
        }

    }
}
