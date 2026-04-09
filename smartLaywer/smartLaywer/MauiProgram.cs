using Microsoft.Extensions.Logging;

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
          //  builder.Services.AddDbContext<LegalManagementContext>(option =>
          //option.UseSqlServer("Data Source=.;Initial Catalog=LegalManagementDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"));
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
