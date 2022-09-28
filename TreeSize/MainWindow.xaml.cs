using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TreeSize.Model;
using TreeSize.ViewModel;

namespace TreeSize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IList<Item> _drives;

        public MainWindow()
        {
            InitializeComponent();

            SetDrives();
            FillComboBox();
        }
        private void FillComboBox()
        {
            var drivesNames = _drives.Select(d => d.Name).ToList();

            foreach (var name in drivesNames)
                ComboBox.Items.Add(name);

            ComboBox.SelectedValue = drivesNames.First();
        }
        private void SetDrives()
        {
            _drives = DriveInfo.GetDrives()
                                .Select(d => new ItemViewModel(d).Item)
                                .ToList();
        }
        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if ((e.OriginalSource as TreeViewItem)?.DataContext is not Item item)
                return;

            item.Items.Clear();

            SetNodes(item);
        }
        private void ButtonGo_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedDrive = _drives.First(d => d.Name == ComboBox.SelectedValue.ToString());
            SetNodes(selectedDrive);

            Root.ItemsSource = selectedDrive.Items;
        }
        private void SetNodes(Item item)
        {
            try
            {
                item.Items = new MainViewModel().SetDirectory(item);

                RunToCountSize(item);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }
        private void RunToCountSize(Item item)
        {
            Directory.EnumerateDirectories(item.FullName)
            .Select(s => new DirectoryInfo(s))
                    .Select(d =>
                        Task.Run(() => new MainViewModel().CountSize(item.Items, d)))
                    .ToArray();
        }
    }
}
