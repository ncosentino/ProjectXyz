using Autofac;

namespace Examples.Modules.BoundedStats
{
    public sealed class BoundedStatsModule : Module
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