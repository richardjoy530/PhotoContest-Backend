using Microsoft.Extensions.Configuration;

namespace PhotoContest.Implementation
{
    /// <summary>
    /// Database
    /// </summary>
    public class Database : IDatabase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Database(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Connection");
        }

        /// <inheritdoc />
        public string ConnectionString { get; set; }
    }
}