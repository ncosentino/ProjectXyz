using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Stats.Bounded.Default.Autofac
{
    public sealed class DepencencyModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ReadOnlyStatDefinitionIdToBoundsMappingRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
