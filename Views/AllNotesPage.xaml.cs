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

  // TODO: move this to the VM
  private async void NotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    if (e.CurrentSelection.Count > 0)
    {
      var note = (Note)e.CurrentSelection[0];

      await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NoteViewModel.ItemId)}={note.FileName}");

      // Unselected the UI
      notesCollection.SelectedItem = null;
    }
  }

  private async void Add_Clicked(object sender, EventArgs e)
  {
    await Shell.Current.GoToAsync(nameof(NotePage), animate: true);
  }
}