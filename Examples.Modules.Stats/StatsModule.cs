using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.Stats
{
    public sealed class StateEnchantmentsModule : SingleRegistrationModule
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