namespace FlyoutPageDemoMaui.Views;

public partial class NotePage : ContentPage
{
  public NotePage()
  {
    InitializeComponent();

    BindingContext = new ViewModels.NoteViewModel();
  }
}