using ET.FileStorage.AzureBlobStorage.Concrete;
using ET.FileStorage.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace ET.FileStorage.AzureBlobStorage
{
    public static class AzureBlobStorageImageServiceRegistration
    {
        public static IServiceCollection AddAzureBlobStorageImageServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, AzureBlobStorageImageServiceAdapter>();
            return services;
        }
    }
}
