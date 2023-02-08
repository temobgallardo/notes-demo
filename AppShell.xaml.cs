using FlyoutPageDemoMaui.Views;

namespace FlyoutPageDemoMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
	}
}
