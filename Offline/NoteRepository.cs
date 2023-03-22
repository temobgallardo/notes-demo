using FlyoutPageDemoMaui.Models;
using LiteDB;
using LiteDB.Async;

namespace FlyoutPageDemoMaui.Offline;

public class NoteRepository : INoteRepository
{
  public const string DbName = "offlineNotes.db";
  public const string DbPassword = "password";

  private static Lazy<LiteDatabase> LazyDatabase => new(
    () => CreateDatabaseStatic(Path.Combine(FileSystem.AppDataDirectory, DbName), DbPassword));

  public NoteRepository()
  {
  }

  public LiteDatabase Context
  {
    get { return LazyDatabase.Value; }
    set { }
  }

  public async Task<bool> DeleteNoteAsync(int id) => Context.GetCollection<Note>().Delete(id);

  public async Task<Note> GetNoteAsync(int id) => Context.GetCollection<Note>().Query().Where(n => n.Id == id).First();

  public async Task<bool> SaveAsync(Note note)
  {
    var notes = Context.GetCollection<Note>();
    note.LastUpdatedDate = DateTime.UtcNow;
    return notes.Upsert(note);
  }

  public async Task<IEnumerable<Note>> GetNotesAsync() => Context.GetCollection<Note>().Query().OrderByDescending(n => n.Date).ToList();

  public async Task Rebuild()
  {
    //You can't simply copy the file while it's open by LiteDB. You have to make sure the file is closed and that the log file is empty by running db.Checkpoint() before copying the file.
    Context.Checkpoint();

    var dbLocation = Path.Combine(FileSystem.AppDataDirectory, DbName);
    var dbBackupLocation = Path.Combine(dbLocation, DatabaseBackupSystem.dbBackupSuffix);
    await DatabaseBackupSystem.StartBackup(dbLocation, dbBackupLocation);

    Context.Rebuild(); //TODO: Check if rebuild worked and if not make use of backup
  }

  public string GetConnectionString(string fileName = DbName, string password = DbPassword)
  {
    return $"FileName={fileName};Password={password}";
  }

  public LiteDatabase CreateDatabase(string fileName, string password, string connectionType = "direct")
  {
    var db = new LiteDatabase($"FileName={fileName};Password={password};Connection={connectionType}")
    {
      CheckpointSize = 500
    };

    db.Pragma(LiteDB.Engine.Pragmas.TIMEOUT, 60);

    var collection = db.GetCollection<Note>();

    // Creating index.
    collection.EnsureIndex(c => c.Id);

    return db;
  }

  private static LiteDatabase CreateDatabaseStatic(string fileName, string password, string connectionType = "direct")
  {
    var db = new LiteDatabase($"FileName={fileName};Password={password};Connection={connectionType}")
    {
      CheckpointSize = 500
    };

    db.Pragma(LiteDB.Engine.Pragmas.TIMEOUT, 60);

    var collection = db.GetCollection<Note>();

    // Creating index.
    collection.EnsureIndex(c => c.Id);

    return db;
  }

  public void Dispose()
  {
    this.Context.Checkpoint();
    this.Context.Commit();
    this.Context.Dispose();
  }
}