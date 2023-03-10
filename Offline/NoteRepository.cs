using FlyoutPageDemoMaui.Models;
using LiteDB;
using LiteDB.Async;

namespace FlyoutPageDemoMaui.Offline;

public class NoteRepository : INoteRepository
{
  public const string DbLocation = "offlineNotes.db";
  private const string DbPassword = "password";

  private static Lazy<LiteDatabase> LazyDatabase => new(
    () => CreateDatabase(Path.Combine(FileSystem.AppDataDirectory, DbLocation), DbPassword));
  private static LiteDatabase Database => LazyDatabase.Value;

  public NoteRepository()
  {
  }

  public async Task<bool> DeleteNoteAsync(int id) => Database.GetCollection<Note>().Delete(id);

  public async Task<Note> GetNoteAsync(int id) => Database.GetCollection<Note>().Query().Where(n => n.Id == id).First();

  public async Task<bool> SaveAsync(Note note)
  {
    var notes = Database.GetCollection<Note>();
    note.LastUpdatedDate = DateTime.UtcNow;
    return notes.Upsert(note);
  }

  public async Task<IEnumerable<Note>> GetNotesAsync() => Database.GetCollection<Note>().Query().OrderByDescending(n => n.Date).ToList();
  
  private static LiteDatabase CreateDatabase(string filename, string password)
  {
    var db = new LiteDatabase($"FileName={filename};Password={password}")
    {
      CheckpointSize = 500
    };

    db.Pragma(LiteDB.Engine.Pragmas.TIMEOUT, 60);

    var collection = db.GetCollection<Note>();

    // Creating index.
    collection.EnsureIndex(c => c.Id);

    return db;
  }
}