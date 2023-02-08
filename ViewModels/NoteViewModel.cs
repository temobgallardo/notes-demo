using System.Windows.Input;
using FlyoutPageDemoMaui.Models;

namespace FlyoutPageDemoMaui.ViewModels;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public class NoteViewModel : ViewModelBase
{
  public const string DefaultFileName = "notes.txt";

  private Note _model;

  public string ItemId { set => LoadNote(value); }

  public NoteViewModel()
  {
    // TODO: Load from db
    if (File.Exists(Model?.FileName))
    {
      Model.Text = File.ReadAllText(Model.FileName);
    }

    string appDataPath = FileSystem.AppDataDirectory;
    string ramdomFileName = $"{Path.GetRandomFileName()}.notes.txt";

    LoadNote(Path.Combine(appDataPath, ramdomFileName));
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

  private async Task SaveAsync()
  {
    await Save();
    await Shell.Current.GoToAsync("..");
  }

  private Task Save()
  {
    // TODO: [DB] add to DB (LiteDb)
    var tcs = new TaskCompletionSource();

    try
    {
      File.WriteAllText(Model.FileName, Model.Text);
      tcs.TrySetResult();
    }
    catch (Exception ex)
    {
      tcs.SetException(ex);
    }

    return tcs.Task;
  }

  private async Task DeleteAsync()
  {
    await Delete();
    await Shell.Current.GoToAsync("..");
  }

  private Task Delete()
  {
    // TODO: [DB] Delete from DB (LiteDb)

    var tcs = new TaskCompletionSource();

    try
    {
      if (File.Exists(Model.FileName))
      {
        File.Delete(Model.FileName);
      }

      Model.Text = string.Empty;

      tcs.TrySetResult();
    }
    catch (Exception e)
    {
      tcs.TrySetException(e);
    }

    return tcs.Task;
  }

  private void LoadNote(string fileName)
  {
    Model = new Note
    {
      FileName = fileName
    };

    if (File.Exists(fileName))
    {
      Model.Date = File.GetCreationTime(fileName);
      Model.Text = File.ReadAllText(fileName);
    }

    // Refresh the Model property
  }
}