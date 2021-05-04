using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.Autofac
{
    public sealed class EnchantmentsGenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EnchantmentLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BaseEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}