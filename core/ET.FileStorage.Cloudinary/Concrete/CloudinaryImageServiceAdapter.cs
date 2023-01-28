using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ET.FileStorage.Core.Abstract;
using ET.FileStorage.Core.Exceptions;
using ET.FileStorage.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CloudinaryInstance = CloudinaryDotNet.Cloudinary;

namespace ET.FileStorage.Cloudinary.Concrete
{
    public class CloudinaryImageServiceAdapter : BaseImageServiceAdapter, IImageService
    {
        private readonly CloudinaryInstance _cloudinary;

        public CloudinaryImageServiceAdapter(IConfiguration configuration)
        {
            Account? account = configuration.GetSection("CloudinaryAccount").Get<Account>();
            _cloudinary = new CloudinaryInstance(account);
            _cloudinary.Api.Secure = true;
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
            string newFilePath = Path.Combine(directory, fileName);

            CheckCloudinaryDirectory(directory);
            CheckExtension(extensions, extension);

            using MemoryStream memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            memoryStream.Position = 0;

            ImageUploadResult result = _cloudinary.Upload(new ImageUploadParams
            {
                File = new FileDescription(newFilePath, memoryStream),
                Folder = directory
            });

            Guard.IsNotNull(result.Error, new CloudinaryDoesNotUploadImageException(result.Error.Message));

            return result.SecureUrl.ToString();
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
            string newFilePath = Path.Combine(directory, fileName);

            await CheckCloudinaryDirectoryAsync(directory);
            CheckExtension(extensions, extension);

            using MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            ImageUploadResult result = await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(newFilePath, memoryStream),
                Folder = directory
            });

            Guard.IsNotNull(result.Error, new CloudinaryDoesNotUploadImageException(result.Error.Message));

            return result.SecureUrl.ToString();
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> imageUrl
        /// </summary>
        public void Delete(params string[] parameters)
        {
            string publicId = GetPublicId(parameters[0]);
            DeletionParams deletionParams = new(publicId);
            DeletionResult result = _cloudinary.Destroy(deletionParams);
            Guard.IsNotNull(result.Error, new CloudinaryDoesNotDeleteImageException(result.Error.Message));
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> imageUrl
        /// </summary>
        public async Task DeleteAsync(params string[] parameters)
        {
            string publicId = GetPublicId(parameters[0]);
            DeletionParams deletionParams = new(publicId);
            DeletionResult result = await _cloudinary.DestroyAsync(deletionParams);
            Guard.IsNotNull(result.Error, new CloudinaryDoesNotDeleteImageException(result.Error.Message));
        }

        private string GetPublicId(string imageUrl)
        {
            int startIndex = imageUrl.LastIndexOf('/') + 1;
            int endIndex = imageUrl.LastIndexOf('.');
            int length = endIndex - startIndex;
            return imageUrl.Substring(startIndex, length);
        }

        private void CheckCloudinaryDirectory(string folder)
        {
            GetFoldersResult folderResult = _cloudinary.RootFolders();
            if (!folderResult.Folders.Where(x => x.Name.Contains(folder)).Any())
            {
                CreateFolderResult result = _cloudinary.CreateFolder(folder);
                Guard.IsNotNull(result.Error, new CloudinaryDoesNotCreateFolderException(result.Error.Message));
            }
        }

        private async Task CheckCloudinaryDirectoryAsync(string folder)
        {
            GetFoldersResult folderResult = await _cloudinary.RootFoldersAsync();
            if (!folderResult.Folders.Where(x => x.Name.Contains(folder)).Any())
            {
                CreateFolderResult result = await _cloudinary.CreateFolderAsync(folder);
                Guard.IsNotNull(result.Error, new CloudinaryDoesNotCreateFolderException(result.Error.Message));
            }
        }
    }
}