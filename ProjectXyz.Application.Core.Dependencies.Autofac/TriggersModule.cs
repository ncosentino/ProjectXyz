using System.Collections.Generic;
using Autofac;
using ProjectXyz.Application.Core.Triggering;

namespace ProjectXyz.Application.Core.Dependencies.Autofac
{
    public sealed class TriggersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TriggerMechanicRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}