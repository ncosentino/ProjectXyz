using System.Collections.Generic;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers.Plugins;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers
{
    public sealed class Plugin : IEnchantmentsAndTriggersPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var durationTriggerMechanicFactory = pluginArgs.GetFirst<IDurationTriggerMechanicFactory>();
            var expiryTriggerMechanicFactory = new ExpiryTriggerMechanicFactory(durationTriggerMechanicFactory);

            EnchantmentTriggerMechanicRegistrars = new[]
            {
                new EnchantmentExpiryTriggerMechanicRegistrar(expiryTriggerMechanicFactory)
            };
        }

        public IReadOnlyCollection<IEnchantmentTriggerMechanicRegistrar> EnchantmentTriggerMechanicRegistrars { get; }
    }
}
