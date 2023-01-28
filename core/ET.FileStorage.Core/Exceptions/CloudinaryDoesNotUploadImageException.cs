namespace ET.FileStorage.Core.Exceptions
{
    public class CloudinaryDoesNotUploadImageException : Exception
    {

        private const string _message = "Cloudinary upload image error occured: {0}";

        public CloudinaryDoesNotUploadImageException(string errorMessage) : base(string.Format(_message, errorMessage))
        {
        }
    }
}
