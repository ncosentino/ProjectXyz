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
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DateParseHandling = DateParseHandling.None
            };
            settings.Converters.Add(new JsonInt32Converter());
            var jsonSerializer = JsonSerializer.Create(settings);

            builder
                .Register(x => new NewtonsoftJsonDeserializer(
                    jsonSerializer,
                    x.Resolve<ICast>()))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .Register(x => new NewtonsoftJsonSerializer(
                   jsonSerializer,
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
