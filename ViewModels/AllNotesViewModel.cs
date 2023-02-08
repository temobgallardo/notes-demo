﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using FlyoutPageDemoMaui.Models;

namespace FlyoutPageDemoMaui.ViewModels;

public class AllNotesViewModel : ViewModelBase
{
  private bool _isRefreshing = true;

  public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

  public AllNotesViewModel() => LoadNotes();

  public ICommand RefreshNotesCommand => new Command(LoadNotes, () => IsRefreshing);

  public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }

  public void LoadNotes()
  {
    IsRefreshing = true;
    Notes.Clear();

    string appDataPath = FileSystem.AppDataDirectory;
    IEnumerable<Note> notes = Directory.
      EnumerateFiles(appDataPath, "*.notes.txt")
      .Select(filename => new Note()
      {
        FileName = filename,
        Date = File.GetCreationTime(filename),
        Text = File.ReadAllText(filename)
      })
      .OrderBy(note => note.Date);

    foreach (var note in notes)
    {
      Notes.Add(note);
    }

    IsRefreshing = false;
  }
}
