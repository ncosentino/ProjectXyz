using Autofac;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Tests.Functional.TestingData.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class EnchantmentIdentifiers : IEnchantmentIdentifiers
    {
        public IIdentifier EnchantmentDefinitionId { get; } = new StringIdentifier("id");
    }
}