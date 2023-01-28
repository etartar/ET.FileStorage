namespace ET.FileStorage.Core.Extensions
{
    public static class Guard
    {
        public static void IsNull(object data, Exception ex)
        {
            if (data is null)
            {
                throw ex;
            }
        }

        public static void IsNotNull(object data, Exception ex)
        {
            if (data is not null)
            {
                throw ex;
            }
        }
    }
}
