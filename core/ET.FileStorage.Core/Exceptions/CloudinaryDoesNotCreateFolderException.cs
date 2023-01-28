namespace ET.FileStorage.Core.Exceptions
{
    public class CloudinaryDoesNotCreateFolderException : Exception
    {
        private const string _message = "Cloudinary create folder error occured: {0}";

        public CloudinaryDoesNotCreateFolderException(string errorMessage) : base(string.Format(_message, errorMessage))
        {
        }
    }
}
