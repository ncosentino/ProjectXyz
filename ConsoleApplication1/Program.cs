using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.ElapsedTime.Duration;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
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
            var hasEnchantmentsBehaviorFactory = lifetimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            var filterContextFactory = lifetimeScope.Resolve<IFilterContextFactory>();

            var skillRepository = lifetimeScope.Resolve<ISkillRepository>();
            var skill = skillRepository
                .GetSkills(filterContextFactory
                    .CreateFilterContextForSingle(new[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("passive-skill-stat1")),
                            true),
                    }))
                .Single();

            var actorFactory = lifetimeScope.Resolve<IActorFactory>();
            var actor = actorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new IBehavior[]
                {
                    new CanEquipBehavior(new[] { new StringIdentifier("left hand") }),
                    new HasSkillsBehavior(new[] { skill }),
                });

            var actorEnchantments = actor.GetOnly<IHasEnchantmentsBehavior>();
            actorEnchantments.AddEnchantments(new IEnchantment[]
            {
                new Enchantment(
                    new IBehavior[]
                    {
                        new EnchantmentTargetBehavior(new StringIdentifier("self")),
                        new HasStatDefinitionIdBehavior() { StatDefinitionId = new StringIdentifier("stat1") },
                        new EnchantmentExpressionBehavior(new CalculationPriority<int>(1), "stat1 + 1"),
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
                new Enchantment(
                    new IBehavior[]
                    {
                        new EnchantmentTargetBehavior(new StringIdentifier("owner")),
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
            var gameEngine = lifetimeScope.Resolve<IAsyncGameEngine>();
            gameEngine.RunAsync(cancellationTokenSource.Token);

            Console.ReadLine();
        }
    }

    public sealed class SkillModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var definitions = new[]
                    {
                        new SkillDefinition(
                            new StringIdentifier("passive-skill-stat1"),
                            new StringIdentifier("self"), //new StringIdentifier("1x1-front"),
                            new IIdentifier[]
                            {
                            },
                            new Dictionary<IIdentifier, double>()
                            {
                                [new StringIdentifier("stat1")] = 10,
                            },
                            new IFilterAttribute[]
                            {
                            }),
                    };

                    var attributeFilter = c.Resolve<IAttributeFilterer>();
                    var repository = new InMemorySkillDefinitionRepository(
                        attributeFilter,
                        definitions);
                    return repository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}