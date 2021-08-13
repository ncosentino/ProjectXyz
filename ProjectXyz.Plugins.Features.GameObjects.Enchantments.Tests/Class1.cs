using System;
using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Tests
{
    public sealed class HasEnchantmentsBehaviorFunctionalTests
    {
        private readonly HasEnchantmentsBehavior _hasEnchantmentsBehavior;

        public HasEnchantmentsBehaviorFunctionalTests()
        {
            var triggerMechanicRegistrar = new TriggerMechanicRegistrar();
            var activeEnchantmentManager = new ActiveEnchantmentManager(
                triggerMechanicRegistrar,
                enchantmentTriggerMechanicFactoryFacade);
            _hasEnchantmentsBehavior = new HasEnchantmentsBehavior(activeEnchantmentManager);
        }

        [Fact]
        private async Task XX_XX_XX()
        {

        }
    }
}
