using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory.Autofac
{
    public sealed class Module : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ReadOnlyEnchantmentDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}