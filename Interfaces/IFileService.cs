using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveJsonToFileAsync(string jsonContent, string folderName, string fileName);
        Task<string> SaveFileAsync(IFormFile file, string folderName, string fileName);
        Task<List<string>> SaveFilesAsync(List<IFormFile> files, string folderName,string subFolder ,string fileName);


        bool DeleteFile(string filePath);
    }
}