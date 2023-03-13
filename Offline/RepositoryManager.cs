namespace FlyoutPageDemoMaui.Offline;

public class RepositoryManager : IRepositoryManager
{
  public RepositoryManager(INoteRepository repository)
  {
    Repository = repository;
  }

  public INoteRepository Repository { get; private set; }

  public void BackupAndRebuild()
  {
  }
}