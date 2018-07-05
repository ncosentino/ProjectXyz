using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Autofac
{
    public sealed class InternalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<DtoJsonSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StringSerializableDtoDataConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StringSerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterBuildCallback(x =>
                {
                    x
                        .Resolve<IEnumerable<IDiscoverableSerializableDtoDataConverter>>()
                        .Foreach(d => SerializableDtoDataConverterService.Instance.Register(
                            d.DeserializableType,
                            d));
                });
            builder
                .RegisterType<SerializableConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    x
                     .Context
                     .Resolve<IEnumerable<IDiscoverableSerializableConverter>>()
                     .Foreach(d => x.Instance.Register(
                         d.Type,
                         d.DtoType,
                         d));
                });
        }
    }
}
