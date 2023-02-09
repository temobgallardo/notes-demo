using FlyoutPageDemoMaui.Models;

namespace FlyoutPageDemoMaui.Offline;

public interface IRepositoryNoteManager
{
  Task<Note> GetNoteAsync(int id);

  Task<bool> UpsertNoteAsync(Note note);

  Task<bool> DeleteNoteAsync(int id);

  Task<bool> UpdateNoteAsync(Note note);

  Task<bool> InsertNoteAsync(Note note);

  Task<bool> GetNotesAsync();
}