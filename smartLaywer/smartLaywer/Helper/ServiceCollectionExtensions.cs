global using ExaminationSystem_API.Repository.UnitWork;
global using smartLaywer.Repository.ClassRepository;

namespace smartLaywer.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services;
        }
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }

    }
}
