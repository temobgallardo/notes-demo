using FlyoutPageDemoMaui.ViewModels;

namespace FlyoutPageDemoMaui.Models;

public class Note : BindableBase
{
  private string _fileName;
  private string _text;
  private DateTime _date;
  private DateTime _lastUpdatedDate;

  public int Id { get; set; }

  public string FileName { get => _fileName; set => SetProperty(ref _fileName, value); }

  public string Text { get => _text; set => SetProperty(ref _text, value); }

  public DateTime Date { get => _date; set => SetProperty(ref _date, value); }

  public DateTime LastUpdatedDate { get => _lastUpdatedDate; set => SetProperty(ref _lastUpdatedDate, value); }
}
