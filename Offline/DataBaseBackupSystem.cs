namespace FlyoutPageDemoMaui.Offline;

public static class DatabaseBackupSystem
{
  public static async Task StartBackup(string dbLocation)
  {
    var directory = Directory.GetParent(dbLocation);

    //var time = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    var backupPath = Path.Combine(directory.FullName, $"-backup.odb");

    //var db = Locator.Current.GetService<AppDbManager>();
    //await db.DoInterruptedOperationAsync(async () =>
    //{
    //  await Observable.Start(() =>
    //  {
    //    File.Copy(dbLocation, backupPath);
    //  });
    //});
    File.Copy(dbLocation, backupPath);
  }
}