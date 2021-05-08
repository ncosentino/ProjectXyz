﻿using Autofac;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Triggering.Autofac
{
    public sealed class TriggeringModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TriggerMechanicRegistrar>()
                .As<ITriggerMechanicRegistrarFacade>() // specifically the facade to avoid circular dependencies
                .SingleInstance();
        }
    }
}