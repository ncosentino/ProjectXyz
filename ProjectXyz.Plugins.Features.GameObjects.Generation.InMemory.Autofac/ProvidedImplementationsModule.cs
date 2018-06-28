using Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.InMemory.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
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
