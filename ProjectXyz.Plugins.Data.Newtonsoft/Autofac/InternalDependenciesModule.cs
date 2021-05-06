using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Autofac
{
    public sealed class InternalDependenciesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<CustomSerializationRegistrar>()
                .AutoActivate()
                .AsSelf();
            builder
                .RegisterType<SerializableConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AlreadySerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnumerableSerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PropertyDirectedSerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ConstructorDirectedSerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MarkerSerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NullSerializableConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
