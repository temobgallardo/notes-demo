using System.Reactive.Linq;
using MetroLog;
using Microsoft.Extensions.Logging;

namespace FlyoutPageDemoMaui.Offline;

public class RepositoryManager : IRepositoryManager
{
  public RepositoryManager(INoteRepository repository, ILogger<RepositoryManager> logger)
  {
    Repository = repository;
    Logger = logger;
  }

  public INoteRepository Repository { get; private set; }
  public ILogger<RepositoryManager> Logger { get; }

  public async Task BackupAndRebuild()
  {
    var recordsBeforeBackup = await Repository.GetNotesAsync();

    var dbLocation = Path.Combine(FileSystem.AppDataDirectory, NoteRepository.DbName);
    var dbBackupLocation = Path.Combine(dbLocation, DatabaseBackupSystem.dbBackupSuffix);

    // You can't simply copy the file while it's open by LiteDB. You have to make sure the file is closed and that the log file is empty by running db.Checkpoint() before copying the file.
    await DoInterruptedOperationAsync(async () =>
    {
      // TODO: Use a thread manager 
      await Observable.Start(() => File.Copy(dbLocation, dbBackupLocation));
    });

    Repository.Context.Checkpoint();
    Repository.Context.Rebuild();

    try
    {
      var recordsAfterBackup = await Repository.GetNotesAsync();
      if (!recordsAfterBackup.Any() && recordsAfterBackup?.Count() != recordsBeforeBackup?.Count() && recordsAfterBackup != recordsBeforeBackup)
      {
        await DoInterruptedOperationAsync(async () =>
        {
          // TODO: Use a thread manager such as Rx or https://github.com/StephenClearyArchive/Nito.Asynchronous
          await Observable.Start(() => File.Replace(dbBackupLocation, dbLocation, default));
        
        });
      }
      else
      {
        File.Delete(dbBackupLocation);
      }
    }
    catch (Exception ex)
    {
      Logger.LogError("Something went wrong when checking health of Data Base Backup, please check the exception for more info", ex);
    }
  }

  public void Dispose()
  {
    this.Repository.Context?.Dispose();
  }

  public async Task DoInterruptedOperationAsync(Func<Task> exe)
  {
    this.Repository.Dispose();

    var dbLocation = Path.Combine(FileSystem.AppDataDirectory, NoteRepository.DbName);
    // connection here is shared, so we will have a read access rights
    this.Repository.Context = Repository.CreateDatabase(dbLocation, NoteRepository.DbPassword, "shared");

    // remember: trans is per thread, so you should use any mechanism to push all your db operations to the same thread
    // I am using Rx.Net to manage thread pools
    this.Repository.Context.BeginTrans();

    await exe();

    this.Repository.Dispose();

    this.Repository.Context = Repository.CreateDatabase(dbLocation, NoteRepository.DbPassword);
  }

  ~RepositoryManager()
  {
    this.Repository.Context?.Dispose();
  }
}