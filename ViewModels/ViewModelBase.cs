using FlyoutPageDemoMaui.Offline;
using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui.ViewModels;

public abstract class ViewModelBase : BindableBase, IDisposable
{

  protected ViewModelBase(INoteRepository repository, ILogger logger)
  {
    Repository = repository;
    Logger = logger;
  }

  public INoteRepository Repository { get; }
  public ILogger Logger { get; }


  // TODO: Inject MainThread for Unit Testing
  protected static void OnMainThread(Func<Task> function) => MainThread.BeginInvokeOnMainThread(async () => await function());

  protected void ExecAndHandleException(Action function)
  {
    try
    {
      function();
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "***Something wrong happened***");
    }
  }

  protected async Task<bool> ExecAndHandleExceptionAsync(Func<Task> function)
  {
    bool success = false;
    try
    {
      await function();
      success = true;
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "***Something wrong happened***");
    }

    return success;
  }

  #region Dispose
  private bool disposedValue;

  protected virtual void Dispose(bool disposing)
  {
    if (!disposedValue)
    {
      if (disposing)
      {
        // TODO: dispose managed state (managed objects)
      }

      // TODO: free unmanaged resources (unmanaged objects) and override finalizer
      // TODO: set large fields to null
      disposedValue = true;
    }
  }

  // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
  // ~ViewModelBase()
  // {
  //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
  //     Dispose(disposing: false);
  // }

  public void Dispose()
  {
    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }
  #endregion
}