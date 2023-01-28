using ET.FileStorage.Core.Abstract;
using ET.FileStorage.Local.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace ET.FileStorage.Local
{
    public static class LocalImageServiceRegistration
    {
        public static IServiceCollection AddLocalImageServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, LocalImageServiceAdapter>();
            return services;
        }
    }
}
