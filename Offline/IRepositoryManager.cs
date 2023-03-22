namespace FlyoutPageDemoMaui.Offline;

public interface IRepositoryManager
{
  Task BackupAndRebuild();
  Task DoInterruptedOperationAsync(Func<Task> func);
}