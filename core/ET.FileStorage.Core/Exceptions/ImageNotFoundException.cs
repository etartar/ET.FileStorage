namespace ET.FileStorage.Core.Exceptions
{
    public class ImageNotFoundException : Exception
    {
        private const string _message = "Image ({0}) cannot be found.";

        public ImageNotFoundException(string filePath) : base(string.Format(_message, filePath))
        {
        }
    }
}
