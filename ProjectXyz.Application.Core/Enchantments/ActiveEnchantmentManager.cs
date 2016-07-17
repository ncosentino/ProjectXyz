using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Expiration;
using ProjectXyz.Application.Interface.Triggering;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class ActiveEnchantmentManager : IActiveEnchantmentManager
    {
        private readonly Dictionary<IEnchantment, List<ITriggerMechanic>> _activeEnchantments;
        private readonly IExpiryTriggerMechanicFactory _expiryTriggerMechanicFactory;
        private readonly ITriggerMechanicRegistrar _triggerMechanicRegistrar;

        public ActiveEnchantmentManager(
            IExpiryTriggerMechanicFactory expiryTriggerMechanicFactory,
            ITriggerMechanicRegistrar triggerMechanicRegistrar)
        {
            _activeEnchantments = new Dictionary<IEnchantment, List<ITriggerMechanic>>();
            _expiryTriggerMechanicFactory = expiryTriggerMechanicFactory;
            _triggerMechanicRegistrar = triggerMechanicRegistrar;
        }

        public IReadOnlyCollection<IEnchantment> Enchantments => _activeEnchantments.Keys;

        public void Add(IEnchantment enchantment)
        {
            if (!_activeEnchantments.ContainsKey(enchantment))
            {
                _activeEnchantments[enchantment] = new List<ITriggerMechanic>();
            }

            foreach (var expiryTrigger in enchantment
                .Components
                .Get<IExpiryTriggerComponent>()
                .Select(x => _expiryTriggerMechanicFactory.Create(
                    x,
                    t => HandleEnchantmentExpired(enchantment, t))))
            {
                _activeEnchantments[enchantment].Add(expiryTrigger);
                _triggerMechanicRegistrar.RegisterTrigger(expiryTrigger);
            }
        }

        public void Remove(IEnchantment enchantment)
        {
            foreach (var triggerMechanic in _activeEnchantments[enchantment])
            {
                _triggerMechanicRegistrar.UnregisterTrigger(triggerMechanic);
            }

            _activeEnchantments.Remove(enchantment);
        }

        private void HandleEnchantmentExpired(IEnchantment enchantment, ITriggerMechanic triggerMechanic)
        {
            if (_activeEnchantments[enchantment].Count == 1)
            {
                _activeEnchantments.Remove(enchantment);
            }
            else if (!_activeEnchantments[enchantment].Remove(triggerMechanic))
            {
                throw new InvalidOperationException($"Attempted to remove trigger '{triggerMechanic}' but the collection did not contain it.");
            }
        }
    }
}