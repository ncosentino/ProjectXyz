﻿using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Mapping.Default.Autofac
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapGameObjectManagerSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapGameObjectManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}