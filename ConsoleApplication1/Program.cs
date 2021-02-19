using System;
using System.Threading;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.ElapsedTime.Duration;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using ProjectXyz.Testing;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var lifetimeScope = new TestLifeTimeScopeFactory().CreateScope();

            var gameEngine = lifetimeScope.Resolve<IAsyncGameEngine>();

            var actorFactory = lifetimeScope.Resolve<IActorFactory>();
            var actor = actorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new[]
                {
                    new CanEquipBehavior(new[] { new StringIdentifier("left hand") })
                });

            var buffable = actor.GetOnly<IBuffableBehavior>();
            buffable.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new IBehavior[]
                    {
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat1") },
                        new EnchantmentExpressionBehavior(new CalculationPriority<int>(1), "stat1 + 1"),
                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(new Interval<double>(5000))),
                        lifetimeScope.Resolve<IAppliesToBaseStat>(),
                    }),
            });

            var activeEnchantmentManagerFactory = lifetimeScope.Resolve<IActiveEnchantmentManagerFactory>();
            var itemActiveEnchantmentManager = activeEnchantmentManagerFactory.Create();
            var itemFactory = lifetimeScope.Resolve<IItemFactory>();
            var item = itemFactory.Create(
                new BuffableBehavior(itemActiveEnchantmentManager),
                new HasEnchantmentsBehavior(itemActiveEnchantmentManager),
                new CanBeEquippedBehavior(new[] { new StringIdentifier("left hand") }));

            var buffableItem = item.GetFirst<IBuffableBehavior>();
            buffableItem.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new IBehavior[]
                    {
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat2") },
                        new EnchantmentExpressionBehavior(new CalculationPriority<int>(1), "stat2 + 1"),
                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(new Interval<double>(5000))),
                    }),
            });

            var canEquip = actor.GetFirst<ICanEquipBehavior>();
            canEquip.TryEquip(
                new StringIdentifier("left hand"),
                item.Behaviors.GetFirst<ICanBeEquippedBehavior>());

            lifetimeScope
                .Resolve<IMutableGameObjectManager>()
                .MarkForAddition(actor);

            var cancellationTokenSource = new CancellationTokenSource();
            gameEngine.RunAsync(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }
}