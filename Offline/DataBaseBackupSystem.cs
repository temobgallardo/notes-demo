using System.Reactive.Linq;

namespace FlyoutPageDemoMaui.Offline;

public static class DatabaseBackupSystem
{
  public const string dbBackupSuffix = "-backup.odb";

  public static async Task StartBackup(string dbLocation, string dbBackupLocation)
  {
    var db = ServiceProvider.Current.GetService<IRepositoryManager>();
    await db.DoInterruptedOperationAsync(async () =>
    {
      // TODO: Use a thread manager 
      await Observable.Start(() => File.Copy(dbLocation, dbBackupLocation));
    });
    //File.Copy(dbLocation, dbBackupLocation);
  }
}