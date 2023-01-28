#region

using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Implementation.Ado;
using PhotoContest.Implementation.Ado.Providers;

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
        services.AddSingleton<IProvider<Submission>, SubmissionProvider>();
        services.AddSingleton<IProvider<UserInfo>, UserInfoProvider>();
        services.AddSingleton<IProvider<VoteInfo>, VoteInfoProvider>();
        services.AddSingleton<IProvider<Contest>, ContestProvider>();
        services.AddSingleton<IProvider<ScoreInfo>, ScoreInfoProvider>();
        services.AddSingleton<IProvider<FileInfo>, FileInfoProvider>();
        services.AddSingleton<IReferenceIdMapper, ReferenceIdProvider>();

        if (isDev)
            services.AddSingleton<IFileService, SystemFileService>();
        else
            services.AddSingleton<IFileService, AzureBlobService>();
    }
}