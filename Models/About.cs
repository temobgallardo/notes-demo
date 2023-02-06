namespace FlyoutPageDemoMaui.Models;

public static class About
{
  public static string Title => AppInfo.Name;

  public static string Version => AppInfo.VersionString;

  public static string MoreInfoUrl => "https://aka.ms/maui";

  public static string Message => "This app is written in XAML and C# with .NET MAUI.";
}