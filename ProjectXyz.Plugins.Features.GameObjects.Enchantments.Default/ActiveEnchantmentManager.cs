using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

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
            Contract.RequiresNotNull(
                enchantments,
                $"{nameof(enchantments)} cannot be null. Use an empty enumerable.");

            foreach (var enchantment in enchantments)
            {
                Contract.RequiresNotNull(
                    enchantment,
                    $"One of the enchantments in the provided argument " +
                    $"'{nameof(enchantments)}' was null.");

                if (!_activeEnchantments.TryGetValue(
                    enchantment,
                    out var triggerMechanicsForEnchantment))
                {
                    triggerMechanicsForEnchantment = new List<ITriggerMechanic>();
                    _activeEnchantments[enchantment] = triggerMechanicsForEnchantment;
                }

                foreach (var enchantmentTriggerMechanicRegistrar in _enchantmentTriggerMechanicRegistrars)
                {
                    var triggers = enchantmentTriggerMechanicRegistrar.RegisterToEnchantment(
                        enchantment,
                        RemoveTriggerMechanicFromEnchantment);
                    foreach (var trigger in triggers)
                    {
                        triggerMechanicsForEnchantment.Add(trigger);

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
            Contract.RequiresNotNull(
                enchantments,
                $"{nameof(enchantments)} cannot be null. Use an empty enumerable.");

            foreach (var enchantment in enchantments)
            {
                Contract.RequiresNotNull(
                    enchantment,
                    $"One of the enchantments in the provided argument " +
                    $"'{nameof(enchantments)}' was null.");

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
            if (!_activeEnchantments.TryGetValue(
                enchantment,
                out var triggerMechanicsForEnchantment))
            {
                throw new InvalidOperationException(
                    $"Attempted to remove trigger '{triggerMechanic}' but the " +
                    $"enchantment '{enchantment}' was not found in the list " +
                    $"of active enchantments.");
            }

            if (triggerMechanicsForEnchantment.Count == 1)
            {
                _activeEnchantments.Remove(enchantment);
            }
            else if (!triggerMechanicsForEnchantment.Remove(triggerMechanic))
            {
                throw new InvalidOperationException(
                    $"Attempted to remove trigger '{triggerMechanic}' but the " +
                    $"collection for enchantment '{enchantment}' did not contain it.");
            }
        }
    }
}