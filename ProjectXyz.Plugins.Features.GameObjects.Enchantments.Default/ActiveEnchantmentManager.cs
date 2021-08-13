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
        private readonly Dictionary<IGameObject, List<ITriggerMechanic>> _activeEnchantmentsAndTriggerMechanics;
        private readonly ITriggerMechanicRegistrarFacade _triggerMechanicRegistrar;
        private readonly IEnchantmentTriggerMechanicFactoryFacade _enchantmentTriggerMechanicFactoryFacade;

        public ActiveEnchantmentManager(
            ITriggerMechanicRegistrarFacade triggerMechanicRegistrar,
            IEnchantmentTriggerMechanicFactoryFacade enchantmentTriggerMechanicFactoryFacade)
        {
            _activeEnchantmentsAndTriggerMechanics = new Dictionary<IGameObject, List<ITriggerMechanic>>();
            _triggerMechanicRegistrar = triggerMechanicRegistrar;
            _enchantmentTriggerMechanicFactoryFacade = enchantmentTriggerMechanicFactoryFacade;
        }

        public IReadOnlyCollection<IGameObject> Enchantments => _activeEnchantmentsAndTriggerMechanics.Keys;

        public void Add(IEnumerable<IGameObject> enchantments)
        {
            Contract.RequiresNotNull(
                enchantments,
                () => $"{nameof(enchantments)} cannot be null. Use an empty enumerable.");

            foreach (var enchantment in enchantments)
            {
                Contract.RequiresNotNull(
                    enchantment,
                    () => $"One of the enchantments in the provided argument " +
                    $"'{nameof(enchantments)}' was null.");

                if (_activeEnchantmentsAndTriggerMechanics.ContainsKey(enchantment))
                {
                    continue;
                }

                var triggerMechanicsForEnchantment = new List<ITriggerMechanic>();
                _activeEnchantmentsAndTriggerMechanics[enchantment] = triggerMechanicsForEnchantment;

                foreach (var trigger in _enchantmentTriggerMechanicFactoryFacade.CreateTriggerMechanicsForEnchantment(
                    enchantment,
                    RemoveTriggerMechanicFromEnchantment,
                    RemoveEnchantment))
                {
                    triggerMechanicsForEnchantment.Add(trigger);

                    if (!_triggerMechanicRegistrar.CanRegister(trigger))
                    {
                        throw new InvalidOperationException(
                            $"Could not register '{trigger}' to " +
                            $"'{_triggerMechanicRegistrar}'.");
                    }

                    _triggerMechanicRegistrar.RegisterTrigger(trigger);
                }
            }
        }

        public void Remove(IEnumerable<IGameObject> enchantments)
        {
            Contract.RequiresNotNull(
                enchantments,
                () => $"{nameof(enchantments)} cannot be null. Use an empty enumerable.");

            foreach (var enchantment in enchantments)
            {
                Contract.RequiresNotNull(
                    enchantment,
                    () => $"One of the enchantments in the provided argument " +
                    $"'{nameof(enchantments)}' was null.");

                if (!_activeEnchantmentsAndTriggerMechanics.TryGetValue(
                    enchantment,
                    out var triggerMechanicsForEnchantment))
                {
                    throw new InvalidOperationException(
                        $"Attempted to remove '{enchantment}' but it was not " +
                        $"found in the list of active enchantments.");
                }

                foreach (var triggerMechanic in triggerMechanicsForEnchantment)
                {
                    _triggerMechanicRegistrar.UnregisterTrigger(triggerMechanic);
                }

                _activeEnchantmentsAndTriggerMechanics.Remove(enchantment);
            }
        }

        private void RemoveTriggerMechanicFromEnchantment(
            IGameObject enchantment,
            ITriggerMechanic triggerMechanic)
        {
            if (!_activeEnchantmentsAndTriggerMechanics.TryGetValue(
                enchantment,
                out var triggerMechanicsForEnchantment))
            {
                throw new InvalidOperationException(
                    $"Attempted to remove trigger '{triggerMechanic}' but the " +
                    $"enchantment '{enchantment}' was not found in the list " +
                    $"of active enchantments.");
            }

            if (!triggerMechanicsForEnchantment.Remove(triggerMechanic))
            {
                throw new InvalidOperationException(
                    $"Attempted to remove trigger '{triggerMechanic}' but the " +
                    $"collection for enchantment '{enchantment}' did not contain it.");
            }

            _triggerMechanicRegistrar.UnregisterTrigger(triggerMechanic);
        }

        private void RemoveEnchantment(IGameObject enchantment)
        {
            if (!_activeEnchantmentsAndTriggerMechanics.TryGetValue(
                enchantment,
                out var triggerMechanics))
            {
                throw new InvalidOperationException(
                    $"Attempted to remove '{enchantment}' but it was not found " +
                    $"in the collection of active enchantments.");
            }

            // take a copy of the trigger mechanics before removing them!
            foreach (var triggerMechanic in triggerMechanics.ToArray())
            {
                RemoveTriggerMechanicFromEnchantment(
                    enchantment,
                    triggerMechanic);
            }

            _activeEnchantmentsAndTriggerMechanics.Remove(enchantment);
        }
    }
}