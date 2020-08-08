using Autofac;

using Newtonsoft.Json;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            var jsonSerializer = new JsonSerializer();
            builder
                .Register(x => new NewtonsoftJsonDeserializer(jsonSerializer))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .Register(x => new NewtonsoftJsonSerializer(jsonSerializer))
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
