using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.MySql.Autofac
{
    public sealed class Module : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EnchantmentDefinitionRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}