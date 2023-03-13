namespace FlyoutPageDemoMaui;

// extracted from David Ortinau's Weather 21 app on https://github.com/davidortinau/WeatherTwentyOne/blob/main/src/WeatherTwentyOne/Services/ServiceExtensions.cs
public static class ServiceProvider
{
  public static T GetService<T>() => Current.GetService<T>();

  public static IServiceProvider Current
    =>
#if WINDOWS10_0_17763_0_OR_GREATER
        MauiWinUIApplication.Current.Services;
#elif ANDROID
        MauiApplication.Current.Services;
#elif IOS || MACCATALYST
        MauiUIApplicationDelegate.Current.Services;
#else
        null;
#endif
}