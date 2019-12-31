using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.BoundedStats
{
    public sealed class BoundedStatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionIdToBoundsMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}