namespace HRApprove.Infrastructure.Persistences.Connections
{
    using System.Data;
    using HRApprove.Infrastructure.Configurations;
    using HRApprove.Infrastructure.Interfaces;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// Represents the implementation of the MySql connection factory.
    /// </summary>
    public class MySqlConnectionFactory : IMySqlConnectionFactory
    {
        private readonly DatabaseConfiguration databaseConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlConnectionFactory"/> class.
        /// </summary>
        /// <param name="config">The database configuration.</param>
        public MySqlConnectionFactory(DatabaseConfiguration config)
        {
            this.databaseConfiguration = config;
        }

        /// <inheritdoc/>
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(this.databaseConfiguration.ConnectionString);
        }
    }
}
