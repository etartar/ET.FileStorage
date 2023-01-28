namespace ET.FileStorage.Core.Exceptions
{
    public class CloudinaryDoesNotDeleteImageException : Exception
    {
        private const string _message = "Cloudinary delete error occured: {0}";

        public CloudinaryDoesNotDeleteImageException(string errorMessage) : base(string.Format(_message, errorMessage))
        {
        }
    }
}
