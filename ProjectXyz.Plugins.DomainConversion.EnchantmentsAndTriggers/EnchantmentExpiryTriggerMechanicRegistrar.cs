using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
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
                .Get<IExpiryTriggerBehavior>()
                .Select(x => _expiryTriggerMechanicFactory.Create(
                    x,
                    t => removeTriggerMechanicCallback(enchantment, t))))
            {
                yield return expiryTrigger;
            }
        }
    }
}