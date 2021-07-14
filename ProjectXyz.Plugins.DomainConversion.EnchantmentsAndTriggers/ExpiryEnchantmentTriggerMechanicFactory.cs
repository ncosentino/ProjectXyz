using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments
{
    public sealed class ExpiryEnchantmentTriggerMechanicFactory : IDiscoverableEnchantmentTriggerMechanicFactory
    {
        private readonly IExpiryTriggerMechanicFactory _expiryTriggerMechanicFactory;

        public ExpiryEnchantmentTriggerMechanicFactory(IExpiryTriggerMechanicFactory expiryTriggerMechanicFactory)
        {
            _expiryTriggerMechanicFactory = expiryTriggerMechanicFactory;
        }

        public IEnumerable<ITriggerMechanic> CreateTriggerMechanicsForEnchantment(
            IGameObject enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback,
            RemoveEnchantmentDelegate removeEnchantmentCallback)
        {
            foreach (var expiryTrigger in enchantment
                .Get<IExpiryTriggerBehavior>()
                .Select(x => _expiryTriggerMechanicFactory.Create(
                    x,
                    t => removeEnchantmentCallback(enchantment))))
            {
                yield return expiryTrigger;
            }
        }
    }
}