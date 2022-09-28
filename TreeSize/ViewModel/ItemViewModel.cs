using System;
using System.IO;
using TreeSize.Model;

namespace TreeSize.ViewModel
{
    public class ItemViewModel
    {
        private const long DefaultSize = 0L;
        private Item _item;
        public Item Item => _item;
        public ItemViewModel(DriveInfo driveInfo)
        {
            if (driveInfo is null)
                throw new ArgumentNullException(nameof(driveInfo));
            GetDrive(driveInfo);
        }
        public ItemViewModel(DirectoryInfo directoryInfo)
        {
            if (directoryInfo is null)
                throw new ArgumentNullException(nameof(directoryInfo));
            GetDirectory(directoryInfo);
            _item.Items.Add(new Item { Name = "Loading..." });
        }
        public ItemViewModel(FileInfo fileInfo)
        {
            if (fileInfo is null)
                throw new ArgumentNullException(nameof(fileInfo));
            GetFile(fileInfo);
        }
        public Item GetDirectory(DirectoryInfo directoryInfo)
        {
            _item = new Item
            {
                Name = directoryInfo.Name,
                FullName = directoryInfo.FullName,
                Size = DefaultSize,
                ImagePath = Path.GetFullPath(@"..\..\..\Images\folder.png")
            };
            return _item;
        }
        public Item GetDrive(DriveInfo driveInfo)
        {
            _item = new Item
            {
                Name = driveInfo.Name,
                FullName = driveInfo.Name
            };
            return _item;
        }
        public Item GetFile(FileInfo fileInfo)
        {
            _item = new Item
            {
                Name = fileInfo.Name,
                FullName = fileInfo.FullName,
                Size = fileInfo.Length,
                ImagePath = Path.GetFullPath(@"..\..\..\Images\file.png")
            };
            return _item;
        }
    }
}
