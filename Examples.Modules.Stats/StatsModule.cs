using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.Stats
{
    public sealed class StateEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionToTermMappingRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}