using FlyoutPageDemoMaui.Models;
using MetroLog.Maui;

namespace FlyoutPageDemoMaui.Views;

public partial class AboutPage : ContentPage
{
  public AboutPage()
  {
    InitializeComponent();

    BindingContext = new About();
  }

  private async void LearnMore_Clicked(object sender, EventArgs e)
  {
    if (sender is Models.About about)
    {
      // Navigate to the specified URL in the system browser.
      await Launcher.Default.OpenAsync(about.MoreInfoUrl);
    }
  }

  private void Metrologs_Clicked(object sender, EventArgs e)
  {
    var logController = (LogController)sender;

    // will show the MetroLogPage by default
    logController.GoToLogsPageCommand?.Execute(null);
  }
}