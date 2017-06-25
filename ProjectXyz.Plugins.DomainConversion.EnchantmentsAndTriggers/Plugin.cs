using System.Collections.Generic;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers.Plugins;
using ProjectXyz.Api.Triggering.Elapsed;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers
{
    public sealed class Plugin : IEnchantmentsAndTriggersPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var durationTriggerMechanicFactory = pluginArgs
                .GetFirst<IComponent<IDurationTriggerMechanicFactory>>()
                .Value;
            var expiryTriggerMechanicFactory = new ExpiryTriggerMechanicFactory(durationTriggerMechanicFactory);

            SharedComponents = new ComponentCollection(new[]
            {
                new GenericComponent<IEnchantmentTriggerMechanicRegistrar>(new EnchantmentExpiryTriggerMechanicRegistrar(expiryTriggerMechanicFactory)),
            });
        }

        public IComponentCollection SharedComponents { get; }
    }
}
