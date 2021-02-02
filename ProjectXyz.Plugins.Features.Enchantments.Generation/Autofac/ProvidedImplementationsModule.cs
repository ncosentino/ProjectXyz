using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<BaseEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DiscoverableEnchantmentGeneratorAutoRegistrar>()
                .AsSelf()
                .AutoActivate();
        }
    }
}