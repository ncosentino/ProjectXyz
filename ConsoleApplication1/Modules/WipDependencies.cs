using System;
using Autofac;
using ConsoleApplication1.Wip;
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
                .RegisterType<NoneLogger>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class NoneLogger : ILogger
    {
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, object data)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, object data)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, object data)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, object data)
        {
            throw new NotImplementedException();
        }
    }
}
