#region

using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="isDev"></param>
    public static void ConfigureServices(this IServiceCollection services, bool isDev)
    {
        services.AddSingleton<IProvider<PhotoEntry>, PhotoEntryProvider>();
        services.AddSingleton<IProvider<Photographer>, PhotographerProvider>();
        services.AddSingleton<IProvider<PhotographerVoteDetails>, PhotographerVoteDetailsProvider>();
        services.AddSingleton<IProvider<PhotoTheme>, PhotoThemeProvider>();
        services.AddSingleton<IProvider<ScoreDetail>, ScoreDetailProvider>();
        services.AddSingleton<IProvider<FileMap>, FileMapProvider>();
        services.AddSingleton<IReferenceIdMapper, ReferenceIdProvider>();

        if (isDev)
            services.AddSingleton<IFileService, SystemFileService>();
        else
            services.AddSingleton<IFileService, AzureBlobService>();
    }
}