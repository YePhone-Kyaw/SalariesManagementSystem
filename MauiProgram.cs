using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SalariesManagementSystem.Services;
using System.Reflection;

namespace SalariesManagementSystem
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();

			string appSettingsFileName = "appsettings.json";
			string appSettingsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), appSettingsFileName);
			builder.Configuration.AddJsonFile(appSettingsFilePath, optional: true, reloadOnChange: true);

			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");	
				});

			builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<IAppService, AppService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif

           
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<ISalaryService, SalaryService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
	}
}
