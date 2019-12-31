using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.StateEnchantments
{
    public sealed class StateEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}