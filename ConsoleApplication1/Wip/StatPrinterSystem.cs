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
using ProjectXyz.Shared.Behaviors;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

namespace ConsoleApplication1.Wip
{
    public sealed class StatPrinterSystem : ISystem
    {
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IBehaviorFinder _behaviorFinder = new BehaviorFinder();
        private readonly IInterval _updateInterval = new Interval<double>(1000);
        private IInterval _elapsed;

        public StatPrinterSystem(IStateContextProvider stateContextProvider)
        {
            _stateContextProvider = stateContextProvider;
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
                Tuple<IHasStatsBehavior, IHasEnchantmentsBehavior> behaviours;
                if (!_behaviorFinder.TryFind(hasBehavior, out behaviours))
                {
                    continue;
                }

                var statCalculationContext =
                    new StatCalculationContext(
                        new GenericComponent<IStateContextProvider>(_stateContextProvider).Yield(),
                        behaviours
                            .Item2
                            .Enchantments
                            .Where(x => !x.Has<IAppliesToBaseStat>()));

                Console.WriteLine($"Base Stat 1: {behaviours.Item1.BaseStats.GetValueOrDefault(new StringIdentifier("stat1"))}");
                Console.WriteLine($"Calc'd Stat 1: {behaviours.Item1.GetStatValue(statCalculationContext, new StringIdentifier("stat1"))}");
                Console.WriteLine($"Base Stat 2: {behaviours.Item1.BaseStats.GetValueOrDefault(new StringIdentifier("stat2"))}");
                Console.WriteLine($"Calc'd Stat 2: {behaviours.Item1.GetStatValue(statCalculationContext, new StringIdentifier("stat2"))}");
                Console.WriteLine($"# Enchantments: {behaviours.Item2.Enchantments.Count}");
                Console.WriteLine("----");
            }
        }
    }
}