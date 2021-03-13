using System;
using System.Threading;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.ElapsedTime.Duration;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

namespace ConsoleApplication1
{
    internal sealed class Program
    {
        public static void Main()
        {
            var lifetimeScope = new TestLifeTimeScopeFactory().CreateScope();
            var hasEnchantmentsBehaviorFactory = lifetimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            var enchantmentFactory = lifetimeScope.Resolve<IEnchantmentFactory>();
            var calculationPriorityFactory = lifetimeScope.Resolve<ICalculationPriorityFactory>();

            var actorFactory = lifetimeScope.Resolve<IActorFactory>();
            var actor = actorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new IBehavior[]
                {
                    new CanEquipBehavior(new[] { new StringIdentifier("left hand") }),
                });

            var actorEnchantments = actor.GetOnly<IHasEnchantmentsBehavior>();
            actorEnchantments.AddEnchantments(new IEnchantment[]
            {
                enchantmentFactory.Create(
                    new IBehavior[]
                    {
                        new EnchantmentTargetBehavior(new StringIdentifier("self")),
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat1") },
                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "stat1 + 1"),
                        //new ExpiryTriggerBehavior(new DurationTriggerBehavior(new Interval<double>(5000))),
                        //lifetimeScope.Resolve<IAppliesToBaseStat>(),
                    }),
            });

            var itemFactory = lifetimeScope.Resolve<IItemFactory>();
            var item = itemFactory.Create(
                hasEnchantmentsBehaviorFactory.Create(),
                new CanBeEquippedBehavior(new[] { new StringIdentifier("left hand") }));

            var itemEnchantments = item.GetFirst<IHasEnchantmentsBehavior>();
            itemEnchantments.AddEnchantments(new IEnchantment[]
            {
                enchantmentFactory.Create(
                    new IBehavior[]
                    {
                        new EnchantmentTargetBehavior(new StringIdentifier("owner")),
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat2") },
                        new EnchantmentExpressionBehavior(calculationPriorityFactory.Create<int>(1), "stat2 + 1"),
                        new ExpiryTriggerBehavior(new DurationTriggerBehavior(new Interval<double>(5000))),
                    }),
            });

            var canEquip = actor.GetFirst<ICanEquipBehavior>();
            canEquip.TryEquip(
                new StringIdentifier("left hand"),
                item.Behaviors.GetFirst<ICanBeEquippedBehavior>());

            lifetimeScope
                .Resolve<IMapGameObjectManager>()
                .MarkForAddition(actor);

            var cancellationTokenSource = new CancellationTokenSource();
            var gameEngine = lifetimeScope.Resolve<IAsyncGameEngine>();
            gameEngine.RunAsync(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }
}