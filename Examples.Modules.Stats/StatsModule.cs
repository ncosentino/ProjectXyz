using Autofac;

namespace Examples.Modules.Stats
{
    public sealed class StateEnchantmentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StatDefinitionToTermMappingRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}