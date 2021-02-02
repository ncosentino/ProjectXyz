using System.Data;

namespace ProjectXyz.Api.Data.Databases
{
    public static class IDbCommandExtensions
    {
        public static IDbDataParameter CreateParameter<T>(this IDbCommand command, string name, T value)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            return param;
        }

        public static IDbDataParameter AddParameter<T>(this IDbCommand command, string name, T value)
        {
            var param = command.CreateParameter(name, value);
            command.Parameters.Add(param);
            return param;
        }
    }
}
