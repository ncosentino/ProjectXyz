using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers
{
    public sealed class EnchantmentExpiryTriggerMechanicRegistrar : IEnchantmentTriggerMechanicRegistrar
    {
        private readonly IExpiryTriggerMechanicFactory _expiryTriggerMechanicFactory;

        public EnchantmentExpiryTriggerMechanicRegistrar(IExpiryTriggerMechanicFactory expiryTriggerMechanicFactory)
        {
            _expiryTriggerMechanicFactory = expiryTriggerMechanicFactory;
        }

        public IEnumerable<ITriggerMechanic> RegisterToEnchantment(
            IEnchantment enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback)
        {
            foreach (var expiryTrigger in enchantment
                .Components
                .Get<IExpiryTriggerComponent>()
                .Select(x => _expiryTriggerMechanicFactory.Create(
                    x,
                    t => removeTriggerMechanicCallback(enchantment, t))))
            {
                yield return expiryTrigger;
            }
        }
    }
}