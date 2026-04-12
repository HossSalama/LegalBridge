using Microsoft.Extensions.Logging;
using smartLaywer.Mapping.CaseMapping;
using smartLaywer.Mapping.FinancialMapping;
using smartLaywer.Mapping.HearingMapping;
using smartLaywer.Repository.UnitWork;
using smartLaywer.Services.ClassService;
using smartLaywer.Services;
using smartLaywer.Helper;

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

            // ── Database ──────────────────────────────────────────────
            builder.Services.AddDbContext<LegalManagementContext>(options =>
                options
                    .UseSqlServer("Data Source=.;Initial Catalog=LegalManagementDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")
                    .ConfigureWarnings(w => w.Ignore(
                        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

            // ── Repositories (IUnitOfWork, IGenericRepository, IFinancialRepository) ──
            builder.Services.AddRepositories();

            // ── AutoMapper (explicit profiles — avoids double-registration conflicts) ──
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CaseProfile>();
                cfg.AddProfile<FinancialProfile>();
                cfg.AddProfile<HearingProfile>();
            });

            // ── Application Services ──────────────────────────────────
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICaseService, CaseService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ILookupService, LookupService>();
            builder.Services.AddScoped<IHearingService, HearingService>();
            builder.Services.AddScoped<IFinancialsService, FinancialsService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
