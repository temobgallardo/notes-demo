using System.Collections.ObjectModel;
using System.Windows.Input;
using FlyoutPageDemoMaui.Extensions;
using FlyoutPageDemoMaui.Models;
using FlyoutPageDemoMaui.Offline;
using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui.ViewModels;

public class AllNotesViewModel : RefreshableViewModel, ILifeCycleAware
{
  private Note _selectedNote;

  public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

  public AllNotesViewModel(INoteRepository repository, ILogger<AllNotesViewModel> logger) : base(repository, logger) => LoadNotes();

  public ICommand RefreshNotesCommand => new Command(LoadNotes, () => IsRefreshing);

  public ICommand AddNoteCommand => new Command(async () => await AddNote());

  public Note SelectedNote
  {
    get => _selectedNote;
    set
    {
      SetProperty(ref _selectedNote, value);

      if (value != null)
      {
        OnMainThread(async () => await Shell.Current.GoToAsync($"{nameof(NoteViewModel)}?{nameof(NoteViewModel.ItemId)}={value.Id}"));
      }
    }
  }

  public void LoadNotes() => ExecWhileRefreshing(async () =>
  {
    var notes = await Repository.GetNotesAsync();
    Notes.ClearAndAddRange(notes);
  });

  public new void OnAppearing() => LoadNotes();

  public void OnDisappearing()
  {
  }

  private async Task AddNote() => await Shell.Current.GoToAsync(nameof(NoteViewModel), animate: true);
}