using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.InMemory.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<InMemoryAttributeFilterer>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
