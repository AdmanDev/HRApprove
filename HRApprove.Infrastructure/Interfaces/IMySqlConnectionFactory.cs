namespace HRApprove.Infrastructure.Interfaces
{
    using System.Data;

    /// <summary>
    /// Represents the MySql connection factory.
    /// </summary>
    public interface IMySqlConnectionFactory
    {
        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <returns>The database connection.</returns>
        IDbConnection CreateConnection();
    }
}
