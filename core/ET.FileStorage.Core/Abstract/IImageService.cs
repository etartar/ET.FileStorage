using Microsoft.AspNetCore.Http;

namespace ET.FileStorage.Core.Abstract
{
    public interface IImageService
    {
        string Upload(IFormFile formFile, string directory, string[] extensions);
        Task<string> UploadAsync(IFormFile formFile, string directory, string[] extensions);
        void Delete(params string[] parameters);
        Task DeleteAsync(params string[] parameters);
    }
}
