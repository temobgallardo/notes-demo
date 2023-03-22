using FlyoutPageDemoMaui.Models;
using LiteDB;

namespace FlyoutPageDemoMaui.Offline;

public interface INoteRepository : IDisposable
{
  LiteDatabase Context { get; set; }

  Task<Note> GetNoteAsync(int id);

  Task<bool> DeleteNoteAsync(int id);

  Task<bool> SaveAsync(Note note);

  Task<IEnumerable<Note>> GetNotesAsync();
  
  string GetConnectionString(string fileName, string password);

  LiteDatabase CreateDatabase(string fileName, string password, string connectionType = "direct");
}