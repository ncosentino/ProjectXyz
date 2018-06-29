using System;
using Autofac;

namespace ProjectXyz.Shared.Framework.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
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
