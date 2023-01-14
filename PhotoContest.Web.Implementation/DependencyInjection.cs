#region

using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Web.Controllers;
using PhotoContest.Web.Implementation.Controllers;

#endregion

namespace PhotoContest.Web.Implementation;

/// <summary>
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IImagesController, ImagesController>();
        services.AddSingleton<IPhotographerController, PhotographerController>();
        services.AddSingleton<IPhotoEntriesController, PhotoEntriesController>();
        services.AddSingleton<IPhotoThemeController, PhotoThemeController>();
    }
}