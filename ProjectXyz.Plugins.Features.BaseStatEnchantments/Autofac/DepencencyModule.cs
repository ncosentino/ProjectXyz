using Autofac;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Autofac
{
    public sealed class DepencencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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
