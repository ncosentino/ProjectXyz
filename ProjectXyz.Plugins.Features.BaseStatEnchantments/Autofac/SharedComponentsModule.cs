using Autofac;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Systems;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Autofac
{
    public sealed class SharedComponentsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StatUpdaterSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AppliesToBaseStat>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
