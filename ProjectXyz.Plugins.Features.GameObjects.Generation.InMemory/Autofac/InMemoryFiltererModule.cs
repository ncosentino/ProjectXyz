using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.InMemory.Autofac
{
    public sealed class InMemoryFiltererModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<InMemoryAttributeFilterer>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
