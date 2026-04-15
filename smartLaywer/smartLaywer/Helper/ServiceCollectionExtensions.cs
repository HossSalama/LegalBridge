
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
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAgendaService, AgendaService>();

            services.AddScoped<IDocumentService, DocumentService>();

            return services;
        }
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FinancialProfile).Assembly);
            return services;
        }

    }
}
