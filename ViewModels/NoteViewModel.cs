using System.Windows.Input;
using FlyoutPageDemoMaui.Models;
using FlyoutPageDemoMaui.Offline;
using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui.ViewModels;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public class NoteViewModel : ViewModelBase
{
  public const string DefaultFileName = "notes.txt";

  private Note _model = new();

  public int ItemId { set => Task.Run(async () => await LoadNote(value)); }

  public NoteViewModel(INoteRepository repository, ILogger<NoteViewModel> logger) : base(repository, logger)
  {
  }

  public ICommand SaveCommand => new Command(async () => await SaveAsync());

  public ICommand DeleteCommand => new Command(async () => await DeleteAsync());

  public Note Model
  {
    get => _model;
    set => SetProperty(ref _model, value);
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);

    if (disposing)
    {
      _model = null;
    }
  }

  private async Task SaveAsync() => await this.ExecAndHandleExceptionAsync(async () =>
  {
    Model.FileName = $"{Path.GetRandomFileName()}.notes.txt";
    await Repository.SaveAsync(Model);
    await Shell.Current.GoToAsync("..");
  });

  private async Task DeleteAsync() => await this.ExecAndHandleExceptionAsync(async () =>
  {
    await Repository.DeleteNoteAsync(Model.Id);

    Model.Text = string.Empty;

    this.RaisePropertyChanged(nameof(Model));
    await Shell.Current.GoToAsync("..");
  });

  private async Task LoadNote(int id) => await this.ExecAndHandleExceptionAsync(async () =>
  {
    Model = await Repository.GetNoteAsync(id);
    this.RaisePropertyChanged(nameof(Model));
  });
}