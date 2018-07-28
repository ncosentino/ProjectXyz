﻿using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Weather.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<WeatherSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherGenerationContextAttributeProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}