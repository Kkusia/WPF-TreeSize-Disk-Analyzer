using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TreeSize.Model;

namespace TreeSize.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Item> SetDirectory(Item item)
        {
            var directory = new DirectoryInfo(item.FullName);
            var items = new ObservableCollection<Item>();

            foreach (var subDir in directory.EnumerateDirectories())
                items.Add(new ItemViewModel(subDir).Item);

            foreach (var file in directory.EnumerateFiles())
                items.Add(new ItemViewModel(file).Item);
            return items;
        }
        public ObservableCollection<Item> CountSize(ObservableCollection<Item> items,
            DirectoryInfo directory)
        {
            foreach (var item in items.Where(n => n.FullName == directory.FullName))
                CountSize(directory, item);

            return items;
        }
        private void CountSize(DirectoryInfo directory, Item item)
        {
            try
            {
                foreach (var file in directory.EnumerateFiles())
                    item.Size += file.Length;

                foreach (var subDir in directory.EnumerateDirectories())
                    CountSize(subDir, item);
            }
            catch (UnauthorizedAccessException)
            {
                Debug.Print(nameof(CountSize) + ": " + directory.FullName);
            }
        }
    }
}
