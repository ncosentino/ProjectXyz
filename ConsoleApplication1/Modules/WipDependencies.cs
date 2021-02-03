using System;
using System.Data;

using Autofac;
using ConsoleApplication1.Wip;

using MySql.Data.MySqlClient;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Logging;
using ProjectXyz.Framework.Autofac;

namespace ConsoleApplication1.Modules
{
    public sealed class WipDependencies : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatPrinterSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ConsoleLogger>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MySqlConnectionFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class MySqlConnectionFactory : IConnectionFactory
        {
            public IDbConnection Create()
            {
                var connection = new MySqlConnection(
                    $"Server=localhost;" +
                    $"Database=macerus;" +
                    $"Uid=macerus;" +
                    $"Pwd=macerus;");
                return connection;
            }
        }

        private sealed class ConsoleLogger : ILogger
        {
            public void Debug(string message) =>
                Debug(message, null);

            public void Debug(string message, object data) =>
                Log("DEBUG", message, data);

            public void Error(string message) =>
                Error(message, null);

            public void Error(string message, object data) =>
                Log("ERROR", message, data);

            public void Info(string message) =>
                Info(message, null);

            public void Info(string message, object data) =>
                Log("INFO", message, data);

            public void Warn(string message) =>
                Warn(message, null);

            public void Warn(string message, object data) =>
                Log("WARN", message, data);

            private void Log(string prefix, string message, object data)
            {
                Console.WriteLine($"{prefix}: {message}");
                if (data != null)
                {
                    Console.WriteLine($"\t{data}");
                }
            }
        }
    }
}
