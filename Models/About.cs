using MetroLog.Maui;

namespace FlyoutPageDemoMaui.Models;

public class About : LogController
{
  public string Title => AppInfo.Name;

  public string Version => AppInfo.VersionString;

  public string MoreInfoUrl => "https://aka.ms/maui";

  public string Message => "This app is written in XAML and C# with .NET MAUI. \nIt is intended for POC creation with multiple tools";
}