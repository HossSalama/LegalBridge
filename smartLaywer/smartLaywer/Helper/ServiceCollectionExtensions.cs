global using ExaminationSystem_API.Repository.UnitWork;
global using smartLaywer.Repository.ClassRepository;
using smartLaywer.Service.ClassService;

namespace smartLaywer.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IFinancialRepository, FinancialRepository>();
            


            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFinancialsService, FinancialsService>();

            return services;
        }
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }

    }
}
