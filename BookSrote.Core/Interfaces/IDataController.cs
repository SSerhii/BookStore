using System.IO;

namespace BookStore.Core.Interfaces
{
    public interface IDataController
    {
        bool FileExists(string fileName);
        bool DirectoryExists(string folderName);
        void CreateFolder(string folderName);
        void DeleteFile(string relativeFilePath);
        Stream GetStream(string relativeFilePath);
    }
}
