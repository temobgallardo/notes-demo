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
  
  public async Task Rebuild()
  {
    //You can't simply copy the file while it's open by LiteDB. You have to make sure the file is closed and that the log file is empty by running db.Checkpoint() before copying the file.
    Database.Checkpoint();

    // TODO: Create db backup. Making copies of the datafile should be fine for most purposes.
    await DatabaseBackupSystem.StartBackup(Path.Combine(FileSystem.AppDataDirectory, DbLocation));

    Database.Rebuild(); //TODO: Check if rebuild worked and if not make use of backup
  }

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