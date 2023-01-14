#region

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

#endregion

namespace WebApi;

/// <summary>
///     Program class
/// </summary>
public abstract class Program
{
    /// <summary>
    ///     Entry function
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    ///     Creates a <see cref="IHostBuilder" />
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}