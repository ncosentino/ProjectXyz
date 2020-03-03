using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProjectXyz.Api.Logging;

namespace ProjectXyz.Game.Tests
{
    public sealed class ConsoleLoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<Logger>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class Logger : ILogger
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
