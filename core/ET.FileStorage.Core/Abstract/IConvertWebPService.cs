namespace ET.FileStorage.Core.Abstract
{
    public interface IConvertWebPService
    {
        string Convert(string newFilePath, string webpImagePath, string webpFileName);
    }
}
