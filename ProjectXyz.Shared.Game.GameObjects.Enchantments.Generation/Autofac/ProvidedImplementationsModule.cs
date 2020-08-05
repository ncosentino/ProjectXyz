using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.Autofac
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
                .RegisterType<DiscoverableEnchantmentGeneratorAutoRegistrar>()
                .AsSelf()
                .AutoActivate();
        }
    }
}