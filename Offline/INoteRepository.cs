using FlyoutPageDemoMaui.Models;

namespace FlyoutPageDemoMaui.Offline;

public interface INoteRepository
{
  Task<Note> GetNoteAsync(int id);

  Task<bool> DeleteNoteAsync(int id);

  Task<bool> SaveAsync(Note note);

  Task<IEnumerable<Note>> GetNotesAsync();
}