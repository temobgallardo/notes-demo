using FlyoutPageDemoMaui.Models;
using FlyoutPageDemoMaui.ViewModels;
using MetroLog.Maui;

namespace FlyoutPageDemoMaui.Views;

public partial class AboutPage : ContentPage
{
  public AboutPage(ViewModelBase<About> vm)
  {
    InitializeComponent();

    BindingContext = vm;
  }
}