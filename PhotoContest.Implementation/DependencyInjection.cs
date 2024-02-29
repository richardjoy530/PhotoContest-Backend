using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Implementation.Ado;
using PhotoContest.Implementation.Ado.DataRecords;
using PhotoContest.Implementation.Ado.Providers;
using PhotoContest.Implementation.Service;
using PhotoContest.Implementation.Service.Files;
using PhotoContest.Services;

namespace PhotoContest.Implementation
{
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
            services.AddSingleton<ISubmissionProvider, SubmissionProvider>();
            services.AddSingleton<IUserInfoProvider, UserInfoProvider>();
            services.AddSingleton<IVoteInfoProvider, VoteInfoProvider>();
            services.AddSingleton<IContestProvider, ContestProvider>();
            services.AddSingleton<IScoreInfoProvider, ScoreInfoProvider>();
            services.AddSingleton<IFileInfoProvider, FileInfoProvider>();

            if (isDev)
                services.AddSingleton<IFileProvider, SystemFileProvider>();
            else
                services.AddSingleton<IFileProvider, AzureBlobProvider>();

            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IContestManagementService, ContestManagementService>();
        }
    }
}