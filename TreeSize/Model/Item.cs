using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TreeSize.Model
{
    public class Item : INotifyPropertyChanged
    {
        private long _size;
        private ObservableCollection<Item> _items;
        public string FullName { get; init; }
        public string ImagePath { get; init; }
        public string Name { get; init; }
        public long Size
        {
            get => _size;
            set
            {
                if (value == _size)
                    return;

                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (value == _items)
                    return;

                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public Item()
        {
            _items = new ObservableCollection<Item>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
