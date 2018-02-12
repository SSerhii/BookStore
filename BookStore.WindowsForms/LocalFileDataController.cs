using System.IO;
using BookStore.Core.Interfaces;

namespace BookStore.WindowsForms
{
    class LocalFileDataController : IDataController
    {
        public bool FileExists(string relativeFilePath)
        {
            return File.Exists(CreateAbsolutePath(relativeFilePath));
        }

        public bool DirectoryExists(string folderName)
        {
            return Directory.Exists(CreateAbsolutePath(folderName));
        }

        public void CreateFolder(string folderName)
        {
            Directory.CreateDirectory(CreateAbsolutePath(folderName));
        }

        private string CreateAbsolutePath(string relativeFilePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativeFilePath);
        }
        
        public void DeleteFile(string relativeFilePath)
        {
            File.Delete(CreateAbsolutePath(relativeFilePath));
        }

        public Stream GetStream(string relativeFilePath)
        {
            var absolutePath = CreateAbsolutePath(relativeFilePath);
            return File.Open(absolutePath, FileMode.OpenOrCreate);
        }
    }
}
