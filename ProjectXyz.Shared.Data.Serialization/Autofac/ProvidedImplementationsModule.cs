using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Data.Serialization.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ObjectToSerializationIdConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    x
                    .Context
                    .Resolve<IEnumerable<IDiscoverableTypedObjectToSerializationIdConverter>>()
                    .Foreach(d =>
                    {
                        foreach (var type in d.ConvertableTypes)
                        {
                            x.Instance.Register(type, d);
                        }
                    });
                });
            builder
               .RegisterType<SerializableIdToTypeConverterFacade>()
               .AsImplementedInterfaces()
               .SingleInstance()
               .OnActivated(x =>
               {
                   x
                   .Context
                   .Resolve<IEnumerable<IDiscoverablSerializationIdToTypeConverter>>()
                   .Foreach(d =>
                   {
                       foreach (var serializableId in d.ConvertableSerializableIds)
                       {
                           x.Instance.Register(serializableId, d);
                       }
                   });
               });
        }
    }
}
