using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class ActiveEnchantmentManager : IActiveEnchantmentManager
    {
        private readonly Dictionary<IGameObject, List<ITriggerMechanic>> _activeEnchantments;
        private readonly ITriggerMechanicRegistrar _triggerMechanicRegistrar;
        private readonly IReadOnlyCollection<IEnchantmentTriggerMechanicRegistrar> _enchantmentTriggerMechanicRegistrars;

        public ActiveEnchantmentManager(
            ITriggerMechanicRegistrar triggerMechanicRegistrar,
            IEnumerable<IEnchantmentTriggerMechanicRegistrar> enchantmentTriggerMechanicRegistrars)
        {
            _activeEnchantments = new Dictionary<IGameObject, List<ITriggerMechanic>>();
            _triggerMechanicRegistrar = triggerMechanicRegistrar;
            _enchantmentTriggerMechanicRegistrars = enchantmentTriggerMechanicRegistrars.ToArray();
        }

        public IReadOnlyCollection<IGameObject> Enchantments => _activeEnchantments.Keys;

        public void Add(IEnumerable<IGameObject> enchantments)
        {
            foreach (var enchantment in enchantments)
            {
                if (!_activeEnchantments.ContainsKey(enchantment))
                {
                    _activeEnchantments[enchantment] = new List<ITriggerMechanic>();
                }

                foreach (var enchantmentTriggerMechanicRegistrar in _enchantmentTriggerMechanicRegistrars)
                {
                    var triggers = enchantmentTriggerMechanicRegistrar.RegisterToEnchantment(
                        enchantment,
                        RemoveTriggerMechanicFromEnchantment);
                    foreach (var trigger in triggers)
                    {
                        _activeEnchantments[enchantment].Add(trigger);

                        if (_triggerMechanicRegistrar.CanRegister(trigger))
                        {
                            _triggerMechanicRegistrar.RegisterTrigger(trigger);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Could not register '{trigger}' to '{_triggerMechanicRegistrar}'.");
                        }
                    }
                }
            }
        }

        public void Remove(IEnumerable<IGameObject> enchantments)
        {
            foreach (var enchantment in enchantments)
            {
                foreach (var triggerMechanic in _activeEnchantments[enchantment])
                {
                    _triggerMechanicRegistrar.UnregisterTrigger(triggerMechanic);
                }

                _activeEnchantments.Remove(enchantment);
            }
        }

        private void RemoveTriggerMechanicFromEnchantment(
            IGameObject enchantment,
            ITriggerMechanic triggerMechanic)
        {
            if (_activeEnchantments[enchantment].Count == 1)
            {
                _activeEnchantments.Remove(enchantment);
            }
            else if (!_activeEnchantments[enchantment].Remove(triggerMechanic))
            {
                throw new InvalidOperationException(
                    $"Attempted to remove trigger '{triggerMechanic}' but the " +
                    $"collection did not contain it.");
            }
        }
    }
}