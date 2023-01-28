using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ET.FileStorage.AzureBlobStorage.Configs;
using ET.FileStorage.Core.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ET.FileStorage.AzureBlobStorage.Concrete
{
    internal class AzureBlobStorageImageServiceAdapter : BaseImageServiceAdapter, IImageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageImageServiceAdapter(IConfiguration configuration)
        {
            AzureBlobStorageClient? azureBlobStorage = configuration.GetSection("AzureBlobStorage").Get<AzureBlobStorageClient>();
            _blobServiceClient = new BlobServiceClient(azureBlobStorage?.ConnectionString);
        }

        /// <summary>
        /// upload image file
        /// </summary>
        /// <param name="formFile">Image file</param>
        /// <param name="directory">Blob Container name</param>
        /// <param name="extensions">Extensions : .jpg, .png, etc</param>
        /// <returns></returns>
        public string Upload(IFormFile formFile, string directory, string[] extensions)
        {
            Guid uniqueName = Guid.NewGuid();
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            string fileName = $"{uniqueName}{extension}";

            CheckExtension(extensions, extension);

            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(directory);
            blobContainerClient.CreateIfNotExists();
            blobContainerClient.SetAccessPolicy(PublicAccessType.BlobContainer);

            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName.ToString());

            using FileStream fileStream = new FileStream(fileName, FileMode.Create);
            blobClient.Upload(fileStream);

            return fileName;
        }

        /// <summary>
        /// upload image file
        /// </summary>
        /// <param name="formFile">Image file</param>
        /// <param name="directory">Blob Container name</param>
        /// <param name="extensions">Extensions : .jpg, .png, etc</param>
        /// <returns></returns>
        public async Task<string> UploadAsync(IFormFile formFile, string directory, string[] extensions)
        {
            Guid uniqueName = Guid.NewGuid();
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            string fileName = $"{uniqueName}{extension}";

            CheckExtension(extensions, extension);

            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(directory);
            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName.ToString());

            using FileStream fileStream = new FileStream(fileName, FileMode.Create);
            await blobClient.UploadAsync(fileStream);

            return fileName;
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> blobContainerName
        /// parameters[1] -> blobName
        /// </summary>
        public void Delete(params string[] parameters)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(parameters[0]);
            BlobClient blobClient = blobContainerClient.GetBlobClient(parameters[1]);
            blobClient.DeleteIfExists();
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> blobContainerName
        /// parameters[1] -> blobName
        /// </summary>
        public async Task DeleteAsync(params string[] parameters)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(parameters[0]);
            BlobClient blobClient = blobContainerClient.GetBlobClient(parameters[1]);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
