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
    MauiProgram.Container.Services.GetService<INoteRepository>().Rebuild();
  }
}