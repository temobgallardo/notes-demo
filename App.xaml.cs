using FlyoutPageDemoMaui.Offline;
using MetroLog.Maui;

namespace FlyoutPageDemoMaui;

public partial class App : Application
{
  public App()
  {
    InitializeComponent();

    MainPage = new AppShell();

    LogController.InitializeNavigation(
        page => MainPage!.Navigation.PushModalAsync(page),
        () => MainPage!.Navigation.PopModalAsync());
  }

  protected override void OnStart()
  {
    // Will this fail on other platforms?
    //MauiProgram.Container.Services.GetService<INoteRepository>().Rebuild();
    // This one get the current ioc service provider given the platform we are on feel more secure
    ServiceProvider.GetService<INoteRepository>().Rebuild();
  }
}