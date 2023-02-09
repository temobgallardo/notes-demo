using FlyoutPageDemoMaui.Models;
using LiteDB.Async;

namespace FlyoutPageDemoMaui.Offline;

public class RepositoryNoteManager : IRepositoryNoteManager
{
  public const string DbLocation = "offlineNotes.db";
  private const string DbPassword = "password";

  private LiteDatabaseAsync database;

  public RepositoryNoteManager()
  {
    var databaseLocation = Path.Combine(FileSystem.AppDataDirectory, DbLocation);
    Task.Run(async () => database = await RepositoryNoteManager.CreateDatabaseAsync(databaseLocation, DbPassword));
  }

  public Task<bool> DeleteNoteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<Note> GetNoteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<bool> InsertNoteAsync(Note note)
  {
    var notes = database.GetCollection<Note>();
    await notes.InsertAsync(note);
    return true;
  }

  public Task<bool> UpdateNoteAsync(Note note)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpsertNoteAsync(Note note)
  {
    throw new NotImplementedException();
  }

  public Task<bool> GetNotesAsync()
  {
    throw new NotImplementedException();
  }

  private static async Task<LiteDatabaseAsync> CreateDatabaseAsync(string filename, string password)
  {
    var db = new LiteDatabaseAsync($"FileName={filename};Password={password}")
    {
      CheckpointSize = 500
    };

    await db.PragmaAsync(LiteDB.Engine.Pragmas.TIMEOUT, 60);

    var collection = db.GetCollection<Note>();

    // Creating index.
    await collection.EnsureIndexAsync(c => c.Id);

    return db;
  }
}