using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<GeneratorAttributeValueSerializerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    x
                     .Context
                     .Resolve<IEnumerable<IDiscoverableGeneratorAttributeValueSerializer>>()
                     .Foreach(d => x.Instance.Register(
                         d.SerializableType,
                         d));
                });
            builder
                .RegisterType<Deserializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
