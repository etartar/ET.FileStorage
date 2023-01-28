using ET.FileStorage.Core.Exceptions;

namespace ET.FileStorage.Core.Abstract
{
    public abstract class BaseImageServiceAdapter
    {
        protected void CheckDirectory(string path)
        {
            new FileInfo(path).Directory?.Create();
        }

        protected void CheckExtension(string[] extensions, string extension)
        {
            if (!extensions.Contains(extension))
                throw new NotSupportedExtensionException(extension);
        }

        protected void CheckFileExist(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ImageNotFoundException(filePath);
        }
    }
}
