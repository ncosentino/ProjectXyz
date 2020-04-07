using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Autofac
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
