namespace HRApprove.Infrastructure.Configurations
{
    /// <summary>
    /// Represents the database configuration.
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConfiguration"/> class.
        /// </summary>
        /// <param name="connectionString">The databas connection string.</param>
        public DatabaseConfiguration(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        public string ConnectionString { get; private set; }
    }
}
