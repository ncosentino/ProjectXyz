using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Autofac
{
    public sealed class DepencencyModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EnchantmentApplier>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatUpdater>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
