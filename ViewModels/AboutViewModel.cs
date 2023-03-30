using System.Windows.Input;
using FlyoutPageDemoMaui.Models;
using FlyoutPageDemoMaui.Offline;
using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui.ViewModels;

public class AboutViewModel : ViewModelBase<About>
{
  public AboutViewModel(INoteRepository repository, ILogger logger, IRepositoryManager repositoryManager) : base(repository, logger)
  {
    RepositoryManager = repositoryManager;
  }

  public ICommand OpenAboutCommand => new Command(async () => await Launcher.Default.OpenAsync(Model.MoreInfoUrl));

  public ICommand OpenMetroLogs => new Command(() => Model.GoToLogsPageCommand?.Execute(null));

  public ICommand BackupAndRebuildCommand => new Command(async () => await BackupAndRebuild());

  public IRepositoryManager RepositoryManager { get; }

  private async Task BackupAndRebuild()
  {
    await RepositoryManager.BackupAndRebuild();
  }
}