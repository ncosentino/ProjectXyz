using System;
using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Framework.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new RandomNumberGenerator(new Random()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
