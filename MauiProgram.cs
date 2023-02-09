using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		RegisterEssentials(builder);

		return builder.Build();
	}

	private static void RegisterEssentials(MauiAppBuilder builder)
	{
		// TODO: Register Essentials here
		//builder.Services.AddSingleton<IGeolocation>(ctx => Geolocation.Default);
	}
}
