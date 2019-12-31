using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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
