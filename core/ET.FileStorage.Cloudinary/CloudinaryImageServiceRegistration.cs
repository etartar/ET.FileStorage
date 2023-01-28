using ET.FileStorage.Cloudinary.Concrete;
using ET.FileStorage.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace ET.FileStorage.Cloudinary
{
    public static class CloudinaryImageServiceRegistration
    {
        public static IServiceCollection AddCloudinaryImageServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, CloudinaryImageServiceAdapter>();
            return services;
        }
    }
}
