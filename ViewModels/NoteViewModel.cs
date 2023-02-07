using System.Windows.Input;
using FlyoutPageDemoMaui.Models;

namespace FlyoutPageDemoMaui.ViewModels;

public class NoteViewModel : ViewModelBase
{
  public const string DefaultFileName = "notes.txt";

  private Note _model;
  readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, DefaultFileName);

  public NoteViewModel()
  {
    // TODO: Load from db
    if (File.Exists(Model.FileName))
    {
      Model.Text = File.ReadAllText(Model.FileName);
    }
  }

  public ICommand SaveCommand => new Command(async () => await Save());

  public ICommand DeleteCommand => new Command(async () => await Delete());

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

  private Task Save()
  {
    // TODO: [DB] add to DB (LiteDb) 
    var tcs = new TaskCompletionSource();

    try
    {
      File.WriteAllText(_fileName, Model.Text);
      tcs.TrySetResult();
    }
    catch (Exception ex)
    {
      tcs.SetException(ex);
    }
    finally
    {
      tcs.SetCanceled();
    }

    return tcs.Task;
  }

  private Task Delete()
  {
    // TODO: [DB] Delete from DB (LiteDb) 

    var tcs = new TaskCompletionSource();

    try
    {
      if (File.Exists(_fileName))
      {
        File.Delete(_fileName);
      }

      Model.Text = string.Empty;

      tcs.TrySetResult();
    }
    catch (Exception e)
    {
      tcs.TrySetException(e);
    }
    finally
    {
      tcs.SetCanceled();
    }

    return tcs.Task;
  }
}