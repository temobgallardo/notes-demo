using FlyoutPageDemoMaui.Offline;
using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui.ViewModels;

public class RefreshableViewModel : ViewModelBase, IIsRefreshable
{
  private bool _isRefreshing = true;

  public RefreshableViewModel(INoteRepository repository, ILogger logger) : base(repository, logger)
  {
  }

  public bool IsRefreshing
  {
    get => _isRefreshing;
    set => SetProperty(ref _isRefreshing, value);
  }

  protected void ExecWhileRefreshing(Action action)
  {
    IsRefreshing = true;
    action();
    IsRefreshing = false;
  }

  public override void OnAppearing() { }
}