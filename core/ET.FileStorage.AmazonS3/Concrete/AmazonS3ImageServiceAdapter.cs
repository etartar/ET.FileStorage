using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using ET.FileStorage.AmazonS3.Configs;
using ET.FileStorage.Core.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ET.FileStorage.AmazonS3.Concrete
{
    internal class AmazonS3ImageServiceAdapter : BaseImageServiceAdapter, IImageService
    {
        private readonly AWSS3Configuration _awsConfig;

        public AmazonS3ImageServiceAdapter(IConfiguration configuration)
        {
            _awsConfig = configuration.GetSection("AWSS3Configuration").Get<AWSS3Configuration>();
        }

        /// <summary>
        /// upload image file
        /// </summary>
        /// <param name="formFile">Image file</param>
        /// <param name="directory">Bucket name</param>
        /// <param name="extensions">Extensions : .jpg, .png, etc</param>
        /// <returns></returns>
        public string Upload(IFormFile formFile, string directory, string[] extensions)
        {
            Guid uniqueName = Guid.NewGuid();
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            string fileName = $"{uniqueName}{extension}";

            CheckExtension(extensions, extension);

            BasicAWSCredentials credentials = new BasicAWSCredentials(_awsConfig.AccessKey, _awsConfig.SecretKey);
            AmazonS3Config config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_awsConfig.Region)
            };

            TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = formFile.OpenReadStream(),
                Key = fileName,
                BucketName = directory,
                CannedACL = S3CannedACL.NoACL
            };

            using (AmazonS3Client client = new AmazonS3Client(credentials, config))
            {
                TransferUtility transferUtility = new TransferUtility(client);
                transferUtility.Upload(uploadRequest);
            }

            return fileName;
        }

        /// <summary>
        /// upload image file
        /// </summary>
        /// <param name="formFile">Image file</param>
        /// <param name="directory">Bucket name</param>
        /// <param name="extensions">Extensions : .jpg, .png, etc</param>
        /// <returns></returns>
        public async Task<string> UploadAsync(IFormFile formFile, string directory, string[] extensions)
        {
            Guid uniqueName = Guid.NewGuid();
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            string fileName = $"{uniqueName}{extension}";

            CheckExtension(extensions, extension);

            BasicAWSCredentials credentials = new BasicAWSCredentials(_awsConfig.AccessKey, _awsConfig.SecretKey);
            AmazonS3Config config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_awsConfig.Region)
            };

            TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = formFile.OpenReadStream(),
                Key = fileName,
                BucketName = directory,
                CannedACL = S3CannedACL.NoACL
            };

            using (AmazonS3Client client = new AmazonS3Client(credentials, config))
            {
                TransferUtility transferUtility = new TransferUtility(client);
                await transferUtility.UploadAsync(uploadRequest);
            }

            return fileName;
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> bucketName
        /// parameters[1] -> key
        /// </summary>
        public void Delete(params string[] parameters)
        {
            BasicAWSCredentials credentials = new BasicAWSCredentials(_awsConfig.AccessKey, _awsConfig.SecretKey);
            AmazonS3Config config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_awsConfig.Region)
            };

            using AmazonS3Client client = new AmazonS3Client(credentials, config);
            client.DeleteObjectAsync(parameters[0], parameters[1]);
        }

        /// <summary>
        /// Delete image
        /// parameters[0] -> bucketName
        /// parameters[1] -> key
        /// </summary>
        public async Task DeleteAsync(params string[] parameters)
        {
            BasicAWSCredentials credentials = new BasicAWSCredentials(_awsConfig.AccessKey, _awsConfig.SecretKey);
            AmazonS3Config config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_awsConfig.Region)
            };

            using AmazonS3Client client = new AmazonS3Client(credentials, config);
            await client.DeleteObjectAsync(parameters[0], parameters[1]);
        }
    }
}
