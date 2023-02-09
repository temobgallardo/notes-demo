using FlyoutPageDemoMaui.Models;
using FlyoutPageDemoMaui.ViewModels;

namespace FlyoutPageDemoMaui.Views;

public partial class AllNotesPage : ContentPage
{
  public AllNotesPage()
  {
    InitializeComponent();

    BindingContext = new AllNotesViewModel();
  }

  protected override void OnAppearing()
  {
    var Model = BindingContext as AllNotesViewModel;
    Model.LoadNotes();
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