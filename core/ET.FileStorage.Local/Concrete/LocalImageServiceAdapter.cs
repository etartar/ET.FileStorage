using ET.FileStorage.Core.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ET.FileStorage.Local.Concrete
{
    internal class LocalImageServiceAdapter : BaseImageServiceAdapter, IImageService
    {
        private readonly IHostEnvironment _environment;

        public LocalImageServiceAdapter(IHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// upload image file
        /// </summary>
        /// <param name="formFile">Image file</param>
        /// <param name="directory">Directory name</param>
        /// <param name="extensions">Extensions : .jpg, .png, etc</param>
        /// <returns></returns>
        public string Upload(IFormFile formFile, string directory, string[] extensions)
        {
            Guid uniqueName = Guid.NewGuid();
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            string fileName = $"{uniqueName}{extension}";
            string newFilePath = Path.Combine(_environment.ContentRootPath, directory, fileName);

            CheckDirectory(newFilePath);
            CheckExtension(extensions, extension);

            using (FileStream stream = new FileStream(newFilePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return fileName;
        }

        /// <summary>
        /// upload image file
        /// </summary>
        /// <param name="formFile">Image file</param>
        /// <param name="directory">Directory name</param>
        /// <param name="extensions">Extensions : .jpg, .png, etc</param>
        /// <returns></returns>
        public async Task<string> UploadAsync(IFormFile formFile, string directory, string[] extensions)
        {
            Guid uniqueName = Guid.NewGuid();
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            string fileName = $"{uniqueName}{extension}";
            string newFilePath = Path.Combine(_environment.ContentRootPath, directory, fileName);

            CheckDirectory(newFilePath);
            CheckExtension(extensions, extension);

            await using (FileStream stream = new FileStream(newFilePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return fileName;
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> directory name
        /// parameters[1] -> filename
        /// </summary>
        public void Delete(params string[] parameters) => DeleteImage(parameters);

        /// <summary>
        /// Delete image
        /// parameters[0] -> directory name
        /// parameters[1] -> filename
        /// </summary>
        public async Task DeleteAsync(params string[] parameters) => await Task.Run(() => DeleteImage(parameters));

        private void DeleteImage(params string[] parameters)
        {
            string filePath = Path.Combine(_environment.ContentRootPath, parameters[0], parameters[1]);
            CheckFileExist(filePath);
            File.Delete(filePath);
        }
    }
}
