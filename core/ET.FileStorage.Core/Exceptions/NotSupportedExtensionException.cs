namespace ET.FileStorage.Core.Exceptions
{
    public class NotSupportedExtensionException : Exception
    {
        private const string _message = "Extension ({0}) cannot supported.";

        public NotSupportedExtensionException(string extension) : base(string.Format(_message, extension))
        {

        }
    }
}
