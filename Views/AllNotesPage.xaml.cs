using FlyoutPageDemoMaui.ViewModels;

namespace FlyoutPageDemoMaui.Views;

public partial class AllNotesPage : ContentPage
{
  public AllNotesPage(AllNotesViewModel vm)
  {
    InitializeComponent();

    // The handler may be null if done this way you need to manage that situations.
    // The other way to inject is using the Shell Routing method to inject the vm
    //var repository = this.Handler.MauiContext.Services.GetService<INoteRepository>();
    BindingContext = vm;
  }

  protected override void OnAppearing()
  {
    var Model = BindingContext as ILifeCycleAware;
    Model.OnAppearing();
  }

  private void NotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    if (e.CurrentSelection.Count > 0)
    {
      // Unselected the UI
      notesCollection.SelectedItem = null;
    }
  }
}