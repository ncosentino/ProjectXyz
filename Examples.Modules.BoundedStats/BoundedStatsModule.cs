using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.BoundedStats
{
    public sealed class BoundedStatsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StatDefinitionIdToBoundsMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}