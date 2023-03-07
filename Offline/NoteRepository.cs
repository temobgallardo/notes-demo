using FlyoutPageDemoMaui.Models;
using LiteDB.Async;

namespace FlyoutPageDemoMaui.Offline;

public class NoteRepository : INoteRepository
{
  
  public const string DbLocation = "offlineNotes.db";
  private const string DbPassword = "password";

  private LiteDatabaseAsync _database;

  public NoteRepository()
  {
    var databaseLocation = Path.Combine(FileSystem.AppDataDirectory, DbLocation);
    Task.Run(async () => _database = await CreateDatabaseAsync(databaseLocation, DbPassword));
  }

  public async Task<bool> DeleteNoteAsync(int id) => await _database.GetCollection<Note>().DeleteAsync(id);

  public async Task<Note> GetNoteAsync(int id) => await _database.GetCollection<Note>().Query().Where(n => n.Id == id).FirstAsync();

  public async Task<bool> SaveAsync(Note note)
  {
    var notes = _database.GetCollection<Note>();
    note.LastUpdatedDate = DateTime.UtcNow;
    await notes.UpsertAsync(note);
    return true;
  }

  public async Task<IEnumerable<Note>> GetNotesAsync() => await _database.GetCollection<Note>().Query().OrderByDescending(n => n.Date).ToListAsync();
  
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