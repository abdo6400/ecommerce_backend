namespace api.Services
{
    public class FileService : IFileService
    {
        private readonly string _baseDirectory;

        public FileService(IWebHostEnvironment env)
        {
            if (env != null)
            {
                _baseDirectory = env.WebRootPath;
                if (!string.IsNullOrEmpty(_baseDirectory))
                {
                    _baseDirectory = env.ContentRootPath;

                    if (!Directory.Exists(_baseDirectory))
                    {
                        Directory.CreateDirectory(_baseDirectory);
                    }
                    _baseDirectory = Path.Combine(_baseDirectory, "Resources");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(env));
            }
        }

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

            var directoryPath = Path.Combine(_baseDirectory, folderName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(directoryPath, uniqueFileName);

            // Ensure that no directory exists with the same path
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
            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }

        public async Task<List<string>> SaveFilesAsync(List<IFormFile> files, string folderName, string subFolder, string fileName)
        {
            var savedFilePaths = new List<string>();

            if (files == null || files.Count == 0)
            {
                return savedFilePaths;
            }

            var directoryPath = Path.Combine(_baseDirectory, folderName, subFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
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

        public async Task<string> SaveJsonToFileAsync(string jsonContent, string folderName, string fileName)
        {
            var directoryPath = Path.Combine(_baseDirectory, folderName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName + ".json");

            // Write the JSON content to the file
            await File.WriteAllTextAsync(filePath, jsonContent);

            // Return the relative file path
            return Path.Combine(folderName, fileName + ".json").Replace("\\", "/");
        }
    }
}
