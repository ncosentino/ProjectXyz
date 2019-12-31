﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Moq;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.Tests.DropTables
{
    public sealed class LootGeneratorFunctionalTests
    {
        private static readonly IContainer _container;
        private static readonly IGeneratorContextFactory _generatorContextFactory;
        private readonly ILootGenerator _lootGenerator;

        static LootGeneratorFunctionalTests()
        {
            var moduleDiscoverer = new ModuleDiscoverer();
            var modules = moduleDiscoverer
                .Discover(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !x.GetType().FullName.Equals("ProjectXyz.Game.Core.Dependencies.Autofac.PluginModule"));
            var dependencyContainerBuilder = new DependencyContainerBuilder();
            _container = dependencyContainerBuilder.Create(modules);
            _generatorContextFactory = _container.Resolve<IGeneratorContextFactory>();
        }

        public LootGeneratorFunctionalTests()
        {
            _lootGenerator = _container.Resolve<ILootGenerator>();

        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void GenerateLoot_Scenario_ExpectedResult(
            string _,
            IGeneratorContext context,
            Predicate<IEnumerable<IGameObject>> validateResults)
        {
            var results = _lootGenerator
                .GenerateLoot(context);
            Assert.True(validateResults(results));
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    "Match Any, Requires Exactly 1 Drop",
                    _generatorContextFactory.CreateGeneratorContext(1, 1),
                    new Predicate<IEnumerable<IGameObject>>(results => results.ToArray().Length == 1),
                },
                new object[]
                {
                    "Match Any, Requires Exactly 5 Drops",
                    _generatorContextFactory.CreateGeneratorContext(5, 5),
                    new Predicate<IEnumerable<IGameObject>>(results => results.ToArray().Length == 5),
                },
                new object[]
                {
                    "Unmatching Filter, Not Required, Requires Exacty Drops",
                    _generatorContextFactory.CreateGeneratorContext(
                        3,
                        3,
                        new GeneratorAttribute(
                            new StringIdentifier("don't match anything"),
                            new StringGeneratorAttributeValue("value"),
                            false)),
                    new Predicate<IEnumerable<IGameObject>>(results => results.ToArray().Length == 3),
                },
                new object[]
                {
                    "Unmatching Filter, Required, Throws Because Can't Drop Enough Items",
                    _generatorContextFactory.CreateGeneratorContext(
                        3,
                        3,
                        new GeneratorAttribute(
                            new StringIdentifier("don't match anything"),
                            new StringGeneratorAttributeValue("value"),
                            true)),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        var exception = Assert.Throws<InvalidOperationException>(() => results.Consume());
                        Assert.StartsWith(
                            "There was no drop table that could be selected from the set of filtered drop tables using context ",
                            exception.Message);
                        return true;
                    }),
                },
                new object[]
                {
                    "Exact Match, Link Single Table, Context Reequires More Drops Than Table Provides",
                    _generatorContextFactory.CreateGeneratorContext(
                        10,
                        10,
                        new GeneratorAttribute(
                            new StringIdentifier("id"),
                            new StringGeneratorAttributeValue("Table C"),
                            true)),
                    new Predicate<IEnumerable<IGameObject>>(results => results.ToArray().Length == 10),
                },
                new object[]
                {
                    "Exact Match, Link Single Table, Context Reequires Fewer Drops Than Table Provides",
                    _generatorContextFactory.CreateGeneratorContext(
                        1,
                        1,
                        new GeneratorAttribute(
                            new StringIdentifier("id"),
                            new StringGeneratorAttributeValue("Table C"),
                            true)),
                    new Predicate<IEnumerable<IGameObject>>(results => results.ToArray().Length == 1),
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class TestDataModule : SingleRegistrationModule
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .Register(x => new InMemoryDropTableRepository(new IDropTable[]
                    {
                        // Match All Item Table, Generates Exactly 3
                        new ItemDropTable(
                            new StringIdentifier("Table A"),
                            3,
                            3),
                        // Match Specific Id, Generates Exactly 3
                        new ItemDropTable(
                            new StringIdentifier("Table B"),
                            3,
                            3,
                            new GeneratorAttribute(
                                new StringIdentifier("id"),
                                new StringGeneratorAttributeValue("Table B"),
                                true).Yield(),
                            Enumerable.Empty<IGeneratorAttribute>()),
                        // LinkedTo Specific, Still Generates
                        new LinkedDropTable(
                            new StringIdentifier("Table C"),
                            2,
                            2,
                            new IWeightedEntry[]
                            {
                                new WightedEntry(1, new StringIdentifier("Table B")),
                            },
                            new GeneratorAttribute(
                                new StringIdentifier("id"),
                                new StringGeneratorAttributeValue("Table C"),
                                true).Yield(),
                            Enumerable.Empty<IGeneratorAttribute>()),
                    }))
                    .AsImplementedInterfaces()
                    .SingleInstance();
                builder
                    .Register(x => new InMemoryItemDefinitionRepository(
                        x.Resolve<IAttributeFilterer>(),
                        new IItemDefinition[0]))
                    .AsImplementedInterfaces()
                    .SingleInstance();
                builder
                    .RegisterType<DummyItemGenerator>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }

            private sealed class DummyItemGenerator : IDiscoverableItemGenerator
            {
                public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();

                public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
                {
                    yield return new Mock<IGameObject>(MockBehavior.Strict).Object;
                }
            }
        }
    }
}
