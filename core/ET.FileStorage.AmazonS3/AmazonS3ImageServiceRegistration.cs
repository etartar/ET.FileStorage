using ET.FileStorage.AmazonS3.Concrete;
using ET.FileStorage.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace ET.FileStorage.AmazonS3
{
    public static class AmazonS3ImageServiceRegistration
    {
        public static IServiceCollection AddAmazonS3ImageServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, AmazonS3ImageServiceAdapter>();
            return services;
        }
    }
}
