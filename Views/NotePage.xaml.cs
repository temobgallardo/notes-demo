using FlyoutPageDemoMaui.ViewModels;

namespace FlyoutPageDemoMaui.Views;

public partial class NotePage : ContentPage
{
  public NotePage(NoteViewModel vm)
  {
    InitializeComponent();

    BindingContext = vm;
  }
}