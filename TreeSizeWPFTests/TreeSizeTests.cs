using TreeSize.Model;
using TreeSize.ViewModel;

namespace TestsForTreeSize
{
    [TestClass]
    public class TreeSizeTests
    {
        [TestMethod]
        public void GetDriveViewTest()
        {
            var path = "C:\\";
            var driveInfo = new DriveInfo(path);
            var expected = new Item
            {
                Name = "C:\\"
            };
            var view = new ItemViewModel(driveInfo);
            var result = view.GetDrive(driveInfo);
            var actual = new Item
            {
                Name = result.Name
            };
            Assert.AreEqual(expected.Name, actual.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullDrivePathTest()
        {
            var path = "";
            var driveInfo = new DriveInfo(path);
            var result = new ItemViewModel(driveInfo);
            var actual = $"The {path} is empty";
            Assert.AreEqual(actual, result);
        }
        [TestMethod]
        public void GetFolderViewTest()
        {
            var path = "C:\\TestFolder";
            var dirInfo = new DirectoryInfo(path);
            var expected = new Item
            {
                FullName = "C:\\TestFolder",
                Name = "TestFolder"
            };
            var view = new ItemViewModel(dirInfo);
            var result = view.GetDirectory(dirInfo);
            var actual = new Item
            {
                FullName = result.FullName,
                Name = result.Name
            };
            Assert.AreEqual(actual.FullName, expected.FullName);
            Assert.AreEqual(actual.Name, expected.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFolderPathTest()
        {
            var path = "";
            var dirInfo = new DirectoryInfo(path);
            var result = new ItemViewModel(dirInfo);
            var actual = $"The {path} is empty";
            Assert.AreEqual(actual, result);
        }
        [TestMethod]
        public void GetFileViewTest()
        {
            var path = "C:\\TestFolder\\file.txt";
            var fileInfo = new FileInfo(path);
            var expected = new Item
            {
                FullName = "C:\\TestFolder\\file.txt",
                Name = "file.txt"
            };
            var view = new ItemViewModel(fileInfo);
            var result = view.GetFile(fileInfo);
            var actual = new Item
            {
                FullName = result.FullName,
                Name = result.Name
            };
            Assert.AreEqual(actual.FullName, expected.FullName);
            Assert.AreEqual(actual.Name, expected.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFilePathTest()
        {
            var path = "";
            var fileInfo = new FileInfo(path);
            var result = new ItemViewModel(fileInfo);
            var actual = $"The {path} is empty";
            Assert.AreEqual(actual, result);
        }
    }
}