using Microsoft.AspNetCore.Http;

namespace Application.Common.Utilities
{
    public class FileHelper
    {
        private static string _currentDirectory = Environment.CurrentDirectory + "\\wwwroot";
        private static string _folderName = "\\Images\\";

        public static string Upload(IFormFile file)
        {
            var fileExists = CheckFileExists(file);
            if (fileExists != true)
            {
                return "Error";
            }

            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            var randomName = Guid.NewGuid().ToString();

            if (typeValid != true)
            {
                return "Error";
            }

            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateImageFile(_currentDirectory + _folderName + randomName + type, file);
            return _folderName + randomName + type.Replace("\\", "/");
        }

        public static string Update(IFormFile file, string imagePath)
        {
            var fileExists = CheckFileExists(file);
            if (fileExists != true)
            {
                return "Error";
            }

            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            var randomName = Guid.NewGuid().ToString();

            if (typeValid != true)
            {
                return "Error";
            }

            DeleteOldImageFile((_currentDirectory + imagePath).Replace("/", "\\"));
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateImageFile(_currentDirectory + _folderName + randomName + type, file);
            return _folderName + randomName + type.Replace("\\", "/");
        }

        public static bool Delete(string path)
        {
             DeleteOldImageFile((_currentDirectory + path).Replace("/", "\\"));
            return true;
        }

        private static bool CheckFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return true;
            }
            return false;
        }

        private static bool CheckFileTypeValid(string type)
        {
            if (type != ".jpeg" && type != ".png" && type != ".jpg")
            {
                return false;
            }
            return true;
        }

        private static void CheckDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream fs = File.Create(directory))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        private static void DeleteOldImageFile(string directory)
        {
            if (File.Exists(directory.Replace("/", "\\")))
            {
                File.Delete(directory.Replace("/", "\\"));
            }

        }

    }
}
