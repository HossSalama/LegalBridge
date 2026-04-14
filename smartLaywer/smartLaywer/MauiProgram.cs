using Microsoft.AspNetCore.Components.Authorization;
﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using smartLaywer.Helper;
using smartLaywer.Mapping.CaseMapping;
using smartLaywer.Mapping.FinancialMapping;
using smartLaywer.Mapping.HearingMapping;
using smartLaywer.Repository.UnitWork;

using smartLaywer.Services;
using smartLaywer.Services.ClassService;

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
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<HostAuthenticationStateProvider>();
            builder.Services.AddSingleton<AuthenticationStateProvider>(sp =>
                sp.GetRequiredService<HostAuthenticationStateProvider>());


            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState(); 
            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();


            builder.Services.AddDbContext<LegalManagementContext>(options =>
                options
                    .UseSqlServer("Data Source =.; Initial Catalog = LegalManagementDB; Integrated Security = True; Encrypt=False; Trust Server Certificate=True")
                    .ConfigureWarnings(w => w.Ignore(
                        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));



#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
