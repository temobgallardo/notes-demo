using System.Collections.ObjectModel;

namespace FlyoutPageDemoMaui.Extensions;

public static class ObservableCollectionExtension
{
  public static void AddRange<T>(this ObservableCollection<T> self, IEnumerable<T> data) where T : class
  {
    foreach (var item in data)
    {
      self.Add(item);
    }
  }

  public static void ClearAndAddRange<T>(this ObservableCollection<T> self, IEnumerable<T> data) where T : class
  {
    self.Clear();
    foreach (var item in data)
    {
      self.Add(item);
    }
  }
}