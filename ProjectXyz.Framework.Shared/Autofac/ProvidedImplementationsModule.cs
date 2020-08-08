using System;
using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Framework.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new RandomNumberGenerator(new Random()))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<Cast>()
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}
