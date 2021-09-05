using System;

using Autofac;

using Newtonsoft.Json;

using NexusLabs.Framework;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Autofac
{
    public sealed partial class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            var jsonKey = Guid.NewGuid();
            builder
                .Register(x =>
                {
                    var contractResolver = x.Resolve<ShorthandSerializableResolver>();
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        DateParseHandling = DateParseHandling.None,
                        ContractResolver = contractResolver,
                    };
                    settings.Converters.Add(new JsonInt32Converter());
                    var jsonSerializer = JsonSerializer.Create(settings);
                    return jsonSerializer;
                })
                .Keyed(jsonKey, typeof(JsonSerializer))
                .SingleInstance();
            builder
                .RegisterType<ShorthandSerializableResolver>()
                .SingleInstance();
            builder
                .RegisterType<DefaultJsonSerializationSettings>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IJsonSerializationSettings))
                .SingleInstance();
            builder
                .Register(x => new NewtonsoftJsonDeserializer(
                    x.ResolveKeyed<JsonSerializer>(jsonKey),
                    x.Resolve<ICast>(),
                    x.Resolve<IJsonSerializationSettings>()))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .Register(x => new NewtonsoftJsonSerializer(
                   x.ResolveKeyed<JsonSerializer>(jsonKey),
                   x.Resolve<IObjectToSerializationIdConverterFacade>()))
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<NewtonsoftJsonRecursiveSerialization>()
                .AutoActivate()
                .AsSelf()
                .OnActivated(x =>
                {
                    var instance = x.Instance;
                    
                    var serializerFacade = x.Context.Resolve<INewtonsoftJsonSerializerFacade>();
                    serializerFacade.RegisterDefaultSerializableConverter(instance.ToSerializable);

                    var deserializerFacade = x.Context.Resolve<INewtonsoftJsonDeserializerFacade>();
                    deserializerFacade.RegisterDefaultDeserializableConverter(instance.FromStream);
                });
        }
    }
}
