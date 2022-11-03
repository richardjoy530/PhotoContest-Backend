using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Models;

namespace PhotoContest.Implementation
{
    public static class DependancyInjection
    {
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
            {
                services.AddSingleton<IFileService, SystemFileService>();
            }
            else
            {
                services.AddSingleton<IFileService, AzureBlobService>();
            }
        }
    }
}
