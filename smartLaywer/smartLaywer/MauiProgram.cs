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
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddMapping();

            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddDbContext<LegalManagementContext>(options =>
                options
                    .UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=LegalManagementDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")
                    .ConfigureWarnings(w => w.Ignore(
                        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug(); 

            return builder.Build();
        }
    }
}