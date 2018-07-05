using Autofac;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder
                .RegisterType<Deserializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<Serializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
