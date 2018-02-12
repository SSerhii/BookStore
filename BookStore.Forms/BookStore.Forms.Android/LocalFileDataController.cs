using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BookStore.Core.Interfaces;
using System.IO;

namespace BookStore.Forms.Droid
{
    class LocalFileDataController : IDataController
    {
        public bool FileExists(string fileName)
        {
            return File.Exists(CreateAbsolutePath(fileName));
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
            return Path.Combine(Xamarin.Forms.Forms.Context.ExternalCacheDir.Path, relativeFilePath);
        }
        public void DeleteFile(string relativeFilePath)
        {
            File.Delete(CreateAbsolutePath(relativeFilePath));
        }
        public Stream GetStream(string relativeFilePath)
        {
            return File.Open(CreateAbsolutePath(relativeFilePath), FileMode.OpenOrCreate);
        }
    }
}