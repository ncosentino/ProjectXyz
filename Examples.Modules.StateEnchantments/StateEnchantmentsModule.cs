using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.StateEnchantments
{
    public sealed class StateEnchantmentsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StateIdToTermRepo>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}