﻿using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;

using Xunit;
using ProjectXyz.Api.Framework;
using System.Collections.Generic;
using ProjectXyz.Shared.Game.Behaviors;
using ProjectXyz.Api.Framework.Events;
using System;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Framework.Autofac;
using Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class StatCalculationServiceTests
    {
        private static readonly TestFixture _fixture;

        static StatCalculationServiceTests()
        {
            _fixture = new TestFixture(new TestData());
        }

        [Fact]
        private void GetStatValue_InventoryBuffEquipmentBuff_ExpectedStat()
        {
            var statId = new StringIdentifier("Stat A");
            var statTerm = new StringIdentifier("STAT_A");

            var itemOnlyEnchantment1 = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 10",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var itemOnlyEnchantment2 = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} * 2",
                    _fixture.CalculationPriorityFactory.Create<int>(2),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var actorOnlyEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 3",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner")));
            var inventoryItemOnlyEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 6",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("owner.owner")));

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                Enumerable.Empty<IBehavior>());
            var equipSlotId = actor
                .GetOnly<ICanEquipBehavior>()
                .SupportedEquipSlotIds
                .First();

            var item = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId),
                    _fixture.HasEnchantmentsBehaviorFactory.Create());
            var equippable = item.GetOnly<ICanBeEquippedBehavior>();
            var itemEnchantmentsBehavior = item.GetOnly<IHasEnchantmentsBehavior>();
            itemEnchantmentsBehavior.AddEnchantments(
                itemOnlyEnchantment1,
                itemOnlyEnchantment2,
                actorOnlyEnchantment);

            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId,
                    equippable);

            var inventoryItem = _fixture
                .ItemFactory
                .Create(_fixture.HasEnchantmentsBehaviorFactory.Create());
            var inventoryItemEnchantmentsBehavior = inventoryItem.GetOnly<IHasEnchantmentsBehavior>();
            inventoryItemEnchantmentsBehavior.AddEnchantments(
                itemOnlyEnchantment1,
                inventoryItemOnlyEnchantment);

            var inventory = actor
                .GetOnly<IHasItemContainersBehavior>()
                .ItemContainers
                .Single(c => c.ContainerId.Equals(new StringIdentifier("Inventory")));

            inventory.TryAddItem(inventoryItem);

            var itemStats = item.GetOnly<IHasStatsBehavior>();
            var actorStats = actor.GetOnly<IHasStatsBehavior>();
            var inventoryItemStats = inventoryItem.GetOnly<IHasStatsBehavior>();

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);

            var statCalculationService = _fixture.StatCalculationService;
            Assert.Equal(
                20,
                statCalculationService.GetStatValue(item, statId, context));
            Assert.Equal(
                10,
                statCalculationService.GetStatValue(inventoryItem, statId, context));
            Assert.Equal(
                29,
                statCalculationService.GetStatValue(actor, statId, context));
        }

        [Fact]
        private void GetStatValue_SocketBuff_ExpectedStat()
        {
            var statId = new StringIdentifier("Stat A");
            var statTerm = new StringIdentifier("STAT_A");

            var fitInSocketEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 10",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));
            var canBeSocketedEnchantment = _fixture
                .EnchantmentFactory
                .CreateExpressionEnchantment(
                    statId,
                    $"{statTerm} + 5",
                    _fixture.CalculationPriorityFactory.Create<int>(1),
                    new EnchantmentTargetBehavior(new StringIdentifier("self")));

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                Enumerable.Empty<IBehavior>());
            var equipSlotId = actor
                .GetOnly<ICanEquipBehavior>()
                .SupportedEquipSlotIds
                .First();

            var socketTypeId = new StringIdentifier("socket type id");

            var canBeSocketedItem = _fixture
                .ItemFactory
                .Create(
                    new ApplySocketEnchantmentsBehavior(),
                    new CanBeSocketedBehavior(new[]
                    {
                        socketTypeId,
                    }),
                    new CanBeEquippedBehavior(equipSlotId),
                    _fixture.HasEnchantmentsBehaviorFactory.Create());
            canBeSocketedItem
                .GetOnly<IHasEnchantmentsBehavior>()
                .AddEnchantments(canBeSocketedEnchantment);
            var equippable = canBeSocketedItem.GetOnly<ICanBeEquippedBehavior>();

            var canFitSocketItem = _fixture
                .ItemFactory
                .Create(
                    new CanFitSocketBehavior(socketTypeId, 1),
                    _fixture.HasEnchantmentsBehaviorFactory.Create());
            canFitSocketItem
                .GetOnly<IHasEnchantmentsBehavior>()
                .AddEnchantments(fitInSocketEnchantment);

            Assert.True(
                canBeSocketedItem
                    .GetOnly<ICanBeSocketedBehavior>()
                    .Socket(canFitSocketItem.GetOnly<ICanFitSocketBehavior>()),
                $"Expected to be able socket '{canFitSocketItem}' into " +
                $"'{canBeSocketedItem}'.");

            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId,
                    equippable);

            var canBeSocketedItemStats = canBeSocketedItem.GetOnly<IHasStatsBehavior>();
            var actorStats = actor.GetOnly<IHasStatsBehavior>();
            var canFitSocketItemStats = canFitSocketItem.GetOnly<IHasStatsBehavior>();

            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);

            var statCalculationService = _fixture.StatCalculationService;
            Assert.Equal(
                15,
                statCalculationService.GetStatValue(canBeSocketedItem, statId, context));
            Assert.Equal(
                10,
                statCalculationService.GetStatValue(canFitSocketItem, statId, context));
            Assert.Equal(
                15,
                statCalculationService.GetStatValue(actor, statId, context));
        }

        [Fact]
        private void GetStatValue_OneSetItemEquipped_NoBonuses()
        {
            var statId = new StringIdentifier("Stat A");

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new IBehavior[]
                {
                    new ItemSetActorEquipmentMonitorBehavior(_fixture.ItemSetManager),
                    new ItemSetActorInventoryMonitorBehavior(_fixture.ItemSetManager)
                });

            var equipSlotId1 = new StringIdentifier("hands");
            var item1 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId1),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item1")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item1"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId1,
                    item1.GetOnly<ICanBeEquippedBehavior>());

            AssertStat(actor, statId, 0);
        }

        [Fact]
        private void GetStatValue_FullSetEquipped_SlotAndSpecificItemAndCountEnchantments()
        {
            var statId = new StringIdentifier("Stat A");

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new IBehavior[]
                {
                    new ItemSetActorEquipmentMonitorBehavior(_fixture.ItemSetManager),
                    new ItemSetActorInventoryMonitorBehavior(_fixture.ItemSetManager)
                });
            
            var equipSlotId1 = new StringIdentifier("hands");
            var item1 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId1),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item1")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item1"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId1,
                    item1.GetOnly<ICanBeEquippedBehavior>());

            var equipSlotId2 = new StringIdentifier("feet");
            var item2 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId2),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item2")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item2"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId2,
                    item2.GetOnly<ICanBeEquippedBehavior>());

            AssertStat(actor, statId, 321);
        }

        [Fact]
        private void GetStatValue_TwoIdenticalSetItemsEquipped_NoBonuses()
        {
            var statId = new StringIdentifier("Stat A");

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new IBehavior[]
                {
                    new ItemSetActorEquipmentMonitorBehavior(_fixture.ItemSetManager),
                    new ItemSetActorInventoryMonitorBehavior(_fixture.ItemSetManager)
                });

            var equipSlotId1 = new StringIdentifier("hands");
            var item1 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId1),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item1")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item1"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId1,
                    item1.GetOnly<ICanBeEquippedBehavior>());

            var equipSlotId2 = new StringIdentifier("feet");
            var item2 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId2),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item1")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item1"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId2,
                    item2.GetOnly<ICanBeEquippedBehavior>());

            AssertStat(actor, statId, 0);
        }

        [Fact]
        private void GetStatValue_FullSetEquippedThenOneUnequipped_NoBonuses()
        {
            var statId = new StringIdentifier("Stat A");

            var actor = _fixture.ActorFactory.Create(
                new TypeIdentifierBehavior(),
                new TemplateIdentifierBehavior(),
                new IdentifierBehavior(),
                new IBehavior[]
                {
                    new ItemSetActorEquipmentMonitorBehavior(_fixture.ItemSetManager),
                    new ItemSetActorInventoryMonitorBehavior(_fixture.ItemSetManager)
                });

            var equipSlotId1 = new StringIdentifier("hands");
            var item1 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId1),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item1")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item1"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId1,
                    item1.GetOnly<ICanBeEquippedBehavior>());

            var equipSlotId2 = new StringIdentifier("feet");
            var item2 = _fixture
                .ItemFactory
                .Create(
                    new CanBeEquippedBehavior(equipSlotId2),
                    _fixture.HasEnchantmentsBehaviorFactory.Create(),
                    new TemplateIdentifierBehavior(new StringIdentifier("ItemSetA_Item2")),
                    new BelongsToItemSetBehavior(
                        new StringIdentifier("ItemSetA"),
                        new StringIdentifier("ItemSetA_Item2"),
                        true));
            actor
                .GetOnly<ICanEquipBehavior>()
                .TryEquip(
                    equipSlotId2,
                    item2.GetOnly<ICanBeEquippedBehavior>());

            actor
                .GetOnly<ICanEquipBehavior>()
                .TryUnequip(
                    equipSlotId1,
                    out var _);

            AssertStat(actor, statId, 0);
        }

        private void AssertStat(
            IGameObject obj, 
            IIdentifier statdefinitionId,
            double expectedValue)
        {
            var context = new StatCalculationContext(
                new IComponent[0],
                new IEnchantment[0]);
            var statCalculationService = _fixture.StatCalculationService;
            var actualValue = statCalculationService.GetStatValue(
                obj,
                statdefinitionId,
                context);
            Assert.True(
                expectedValue == actualValue,
                $"Stat '{statdefinitionId}' on '{obj}' was incorrect\r\n" +
                $"\tExpected: {expectedValue}\r\n" +
                $"\tActual: {actualValue}");
        }
    }

    public sealed class TestModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ItemSetManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemSetDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var calculationPriorityFactory = c.Resolve<ICalculationPriorityFactory>();
                    var enchantmentTemplate = new EnchantmentTemplate(c.Resolve<ICalculationPriorityFactory>());
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateExpressionEnchantment(
                            new StringIdentifier("ItemSetA_Item1"),
                            new StringIdentifier("Stat A"),
                            1,
                            "STAT_A + 1"),
                        enchantmentTemplate.CreateExpressionEnchantment(
                            new StringIdentifier("ItemSetA_Count2"),
                            new StringIdentifier("Stat A"),
                            1,
                            "STAT_A + 20"),
                        enchantmentTemplate.CreateExpressionEnchantment(
                            new StringIdentifier("ItemSetA_Feet"),
                            new StringIdentifier("Stat A"),
                            1,
                            "STAT_A + 300"),
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .Register(c =>
                {
                    var itemSetDefinitions = new[]
                    {
                        new ItemSetDefinition(
                            new StringIdentifier("ItemSetA"),
                            new Dictionary<int, IReadOnlyCollection<IIdentifier>>()
                            {
                                [2] = new IIdentifier[] { new StringIdentifier("ItemSetA_Count2") },
                            },
                            true,
                            new[]
                            {
                                new ItemSetMatchingEquipSlotEnchantments(
                                    new StringIdentifier("feet"),
                                    new IIdentifier[] { new StringIdentifier("ItemSetA_Feet") },
                                    2)
                            },
                            new[]
                            {
                                new ItemSetMatchingItemEnchantments(
                                    new StringIdentifier("ItemSetA_Item1"),
                                    new IIdentifier[] { new StringIdentifier("ItemSetA_Item1") },
                                    2)
                            }),
                    };
                    var repository = new InMemoryItemSetDefinitionRepository(itemSetDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }

    public sealed class EnchantmentTemplate
    {
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;

        public EnchantmentTemplate(ICalculationPriorityFactory calculationPriorityFactory)
        {
            _calculationPriorityFactory = calculationPriorityFactory;
        }

        public IEnchantmentDefinition CreateExpressionEnchantment(
            IIdentifier enchantmentDefinitionId,
            IIdentifier statDefinitionId,
            int calculationPriority,
            string expression) =>
            CreateExpressionEnchantment(
                enchantmentDefinitionId,
                statDefinitionId,
                calculationPriority,
                expression,
                Enumerable.Empty<IBehavior>());

        public IEnchantmentDefinition CreateExpressionEnchantment(
            IIdentifier enchantmentDefinitionId,
            IIdentifier statDefinitionId,
            int calculationPriority,
            string expression,
            IEnumerable<IBehavior> behaviors)
        {
            var enchantmentDefinition = new EnchantmentDefinition(
                new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new IdentifierFilterAttributeValue(enchantmentDefinitionId),
                        true),
                },
                new IFilterComponent[]
                {
                    new BehaviorFilterComponent(
                        Enumerable.Empty<IFilterAttribute>(),
                        new IBehavior[]
                        {
                            new EnchantmentTargetBehavior(new StringIdentifier("self")),
                            new EnchantmentExpressionBehavior(
                                _calculationPriorityFactory.Create<int>(calculationPriority),
                                expression),
                            new HasStatDefinitionIdBehavior()
                            {
                                StatDefinitionId = statDefinitionId,
                            },
                        }.Concat(behaviors)),
                });
            return enchantmentDefinition;
        }

        public sealed class EnchantmentDefinition : IEnchantmentDefinition
        {
            public EnchantmentDefinition(
                IEnumerable<IFilterAttribute> attributes,
                IEnumerable<IFilterComponent> filterComponents)
            {
                SupportedAttributes = attributes.ToArray();
                FilterComponents = filterComponents.ToArray();
            }

            public IEnumerable<IFilterAttribute> SupportedAttributes { get; set; }

            public IEnumerable<IFilterComponent> FilterComponents { get; set; }
        }
    }
}
