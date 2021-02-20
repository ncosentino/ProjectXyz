using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Behaviors;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

namespace ConsoleApplication1.Wip
{
    public sealed class StatPrinterSystem : ISystem
    {
        private readonly IStatCalculationService _statCalculationService;
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IBehaviorFinder _behaviorFinder = new BehaviorFinder();
        private readonly IInterval _updateInterval = new Interval<double>(1000);
        private IInterval _elapsed;

        public StatPrinterSystem(
            IStateContextProvider stateContextProvider,
            IStatCalculationService statCalculationService)
        {
            _stateContextProvider = stateContextProvider;
            _statCalculationService = statCalculationService;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .Components
                .Get<IComponent<IElapsedTime>>()
                .First()
                .Value
                .Interval;
            _elapsed = _elapsed == null
                ? elapsed
                : _elapsed.Add(elapsed);
            if (_elapsed.CompareTo(_updateInterval) < 0)
            {
                return;
            }

            _elapsed = null;

            foreach (var hasBehavior in hasBehaviors)
            {
                if (!_behaviorFinder.TryFind(
                    hasBehavior,
                    out Tuple<IHasStatsBehavior, IHasEnchantmentsBehavior> behaviours))
                {
                    continue;
                }

                var hasStatsBehavior = behaviours.Item1;
                var hasEnchantmentsbehavior = behaviours.Item2;

                var statCalculationContext =
                    new StatCalculationContext(
                        new GenericComponent<IStateContextProvider>(_stateContextProvider).Yield(),
                        hasEnchantmentsbehavior
                            .Enchantments
                            .Where(x => !x.Has<IAppliesToBaseStat>()));

                Console.WriteLine($"Base Stat 1: {hasStatsBehavior.BaseStats.GetValueOrDefault(new StringIdentifier("stat1"))}");
                Console.WriteLine($"Calc'd Stat 1: {_statCalculationService.GetStatValue(hasStatsBehavior.Owner, new StringIdentifier("stat1"), statCalculationContext)}");
                Console.WriteLine($"Base Stat 2: {hasStatsBehavior.BaseStats.GetValueOrDefault(new StringIdentifier("stat2"))}");
                Console.WriteLine($"Calc'd Stat 2: {_statCalculationService.GetStatValue(hasStatsBehavior.Owner, new StringIdentifier("stat2"), statCalculationContext)}");
                Console.WriteLine($"# Enchantments: {hasEnchantmentsbehavior.Enchantments.Count}");
                Console.WriteLine("----");
            }
        }
    }
}