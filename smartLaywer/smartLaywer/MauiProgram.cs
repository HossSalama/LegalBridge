using Microsoft.Extensions.Logging;
using smartLaywer.Mapping.CaseMapping;
using smartLaywer.Mapping.FinancialMapping;
using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.ClassService;
using smartLaywer.Services;
namespace smartLaywer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Database
            builder.Services.AddDbContext<LegalManagementContext>(options =>
                options
                    .UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=LegalManagementDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")
                    .ConfigureWarnings(w => w.Ignore(
                        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // ✅ AutoMapper - حدد الـ assemblies بشكل صريح
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Services
            //builder.Services.AddScoped<ICaseService, CaseService>();
            //builder.Services.AddScoped<ILookupService, LookupService>();
            //builder.Services.AddScoped<IClientService, ClientService>();
            //builder.Services.AddScoped<IFinancialsService, FinancialsService>();
            builder.Services.AddScoped<IHearingService, HearingService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug(); // ← واحدة بس كفاية
#endif
            return builder.Build();
        }
    }
}