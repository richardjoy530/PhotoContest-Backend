using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Web.Implementation.Controllers;
using PhotoContest.Web.Controllers;

namespace PhotoContest.Web.Implementation
{
    public static class DependancyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IImagesController, ImagesController>();
            services.AddSingleton<IPhotographerController, PhotographerController>();
            services.AddSingleton<IPhotoEntriesController, PhotoEntriesController>();
            services.AddSingleton<IPhotoThemeController, PhotoThemeController>();
        }
    }
}
