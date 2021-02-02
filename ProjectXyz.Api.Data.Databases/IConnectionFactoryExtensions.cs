using System.Data;

namespace ProjectXyz.Api.Data.Databases
{
    public static class IConnectionFactoryExtensions
    {
        public static IDbConnection OpenNew(this IConnectionFactory connectionFactory)
        {
            var connection = connectionFactory.Create();
            connection.Open();
            return connection;
        }
    }
}
