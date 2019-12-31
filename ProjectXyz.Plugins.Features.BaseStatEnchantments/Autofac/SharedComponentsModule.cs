using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Systems;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Autofac
{
    public sealed class SharedComponentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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
