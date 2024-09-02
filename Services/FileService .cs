using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.Services
{
    public class FileService : IFileService
    {
        private readonly string _baseDirectory;

        public FileService(IWebHostEnvironment env)
        {
            _baseDirectory = Path.Combine(env.WebRootPath);
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }
        //images/categories/05e0d79b-e124-47ba-95ad-c108d1fe0328.jpg
        public bool DeleteFile(string filePath)
        {
            var imagePath = Path.Combine(_baseDirectory, filePath);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }
            return false;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName, string fileName)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            // Generate a unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var imagePathFolder = Path.Combine(_baseDirectory, folderName, fileName);
            if (!Directory.Exists(imagePathFolder))
            {
                Directory.CreateDirectory(imagePathFolder);
            }

            var filePath = Path.Combine(imagePathFolder, uniqueFileName);

            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
            }
            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative path
            return Path.Combine(folderName, fileName, uniqueFileName).Replace("\\", "/");
        }

        public async Task<List<string>> SaveFilesAsync(List<IFormFile> files, string folderName, string subFolder, string fileName)
        {
            var savedFilePaths = new List<string>();

            if (files == null || files.Count == 0)
            {
                return savedFilePaths;
            }

            // Ensure the target directory exists
            var directoryPath = Path.Combine(_baseDirectory, folderName, subFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // Generate a unique file name
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(directoryPath, uniqueFileName);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Add the relative path to the list
                    savedFilePaths.Add(Path.Combine(folderName, subFolder, uniqueFileName).Replace("\\", "/"));
                }
            }

            return savedFilePaths;
        }

    }
}