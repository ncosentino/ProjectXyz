using System.Data;

namespace ProjectXyz.Api.Data.Databases
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
