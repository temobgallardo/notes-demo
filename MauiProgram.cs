using FlyoutPageDemoMaui.Offline;
using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
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

    builder.Logging
#if DEBUG
                .AddTraceLogger(
                    options =>
                    {
                      options.MinLevel = LogLevel.Trace;
                      options.MaxLevel = LogLevel.Critical;
                    }) // Will write to the Debug Output
#endif
                .AddInMemoryLogger(
                    options =>
                    {
                      options.MaxLines = 1024;
                      options.MinLevel = LogLevel.Debug;
                      options.MaxLevel = LogLevel.Critical;
                    })
#if RELEASE
            .AddStreamingFileLogger(
                options =>
                {
                    options.RetainDays = 2;
                    options.FolderPath = Path.Combine(
                        FileSystem.CacheDirectory,
                        "MetroLogs");
                })
#endif
                .AddConsoleLogger(
                    options =>
                    {
                      options.MinLevel = LogLevel.Information;
                      options.MaxLevel = LogLevel.Critical;
                    }); // Will write to the Console Output (logcat for android)

    builder.Services.AddSingleton(LogOperatorRetriever.Instance);

    RegisterEssentials(builder);

    return builder
      .RegisterView()
      .RegisterViewModels()
      .RegisterAppServices()
      .Build();
  }

  private static void RegisterEssentials(this MauiAppBuilder self)
  {
    // TODO: Register Essentials here
    //builder.Services.AddSingleton<IGeolocation>(ctx => Geolocation.Default);
  }

  public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder self)
  {
    self.Services.AddSingleton<ViewModels.AllNotesViewModel>();

    self.Services.AddTransient<ViewModels.NoteViewModel>();

    return self;
  }

  public static MauiAppBuilder RegisterView(this MauiAppBuilder self)
  {
    self.Services.AddSingleton<Views.AllNotesPage>();
    self.Services.AddSingleton<Views.AboutPage>();

    self.Services.AddTransient<Views.NotePage>();

    return self;
  }

  public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder self)
  {
    self.Services.AddSingleton<INoteRepository, NoteRepository>();
    self.Services.AddLogging();
    return self;
  }
}