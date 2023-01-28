namespace ET.FileStorage.AzureBlobStorage.Configs
{
    public class AzureBlobStorageClient
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string EndpointSuffix { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"DefaultEndpointsProtocol=https;AccountName={AccountName};AccountKey={AccountKey};EndpointSuffix={EndpointSuffix}";
            }
        }
    }
}
