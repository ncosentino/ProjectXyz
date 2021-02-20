﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Moq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Plugins.Features.TimeOfDay.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Behaviors.Filtering;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Items.Generation.DropTables
{
    public sealed class LootGeneratorFunctionalTests
    {
        private static readonly IFilterContextFactory _filterContextFactory;
        private static readonly IFilterContextProvider _filterContextProvider;
        private static readonly ILootGenerator _lootGenerator;

        static LootGeneratorFunctionalTests()
        {
            _filterContextFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IFilterContextFactory>();
            _filterContextProvider = CachedDependencyLoader.LifeTimeScope.Resolve<IFilterContextProvider>();
            _lootGenerator = CachedDependencyLoader.LifeTimeScope.Resolve<ILootGenerator>();
        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void GenerateLoot_Scenario_ExpectedResult(
            string _,
            IFilterContext context,
            Predicate<IEnumerable<IGameObject>> validateResults)
        {
            var results = _lootGenerator.GenerateLoot(context);
            Assert.True(validateResults(results));
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    "Request actor stat drop table, no actor present, throws",
                    _filterContextProvider
                        .GetContext()
                        .WithRange(1, 1)
                        .WithAdditionalAttributes(new[]
                        {
                            new FilterAttribute(
                                new StringIdentifier("drop-table"),
                                new IdentifierFilterAttributeValue(new StringIdentifier("Actor Stats Table")),
                                true),
                        }),
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
                    "Match Any, Requires Exactly 1 Drop",
                    _filterContextFactory.CreateContext(1, 1),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Single(results);
                        return true;
                    }),
                },
                new object[]
                {
                    "Match Any, Requires Exactly 5 Drops",
                    _filterContextFactory.CreateContext(5, 5),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Equal(5, results.Count());
                        return true;
                    }),
                },
                new object[]
                {
                    "Exact Match, Requires Exactly 5 Drops",
                    _filterContextFactory.CreateContext(
                        5,
                        5,
                        new FilterAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("Table B")),
                            true)),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Equal(5, results.Count());
                        return true;
                    }),
                },
                new object[]
                {
                    "Unmatching Filter, Not Required, Requires Exactly 3 Drops",
                    _filterContextFactory.CreateContext(
                        3,
                        3,
                        new FilterAttribute(
                            new StringIdentifier("don't match anything"),
                            new StringFilterAttributeValue("value"),
                            false)),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Equal(3, results.Count());
                        return true;
                    }),
                },
                new object[]
                {
                    "Unmatching Filter, Required, Throws Because Can't Drop Enough Items",
                    _filterContextFactory.CreateContext(
                        3,
                        3,
                        new FilterAttribute(
                            new StringIdentifier("don't match anything"),
                            new StringFilterAttributeValue("value"),
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
                    "Exact Match, Link Single Table, Context Requires More Drops Than Table Provides",
                    _filterContextFactory.CreateContext(
                        10,
                        10,
                        new FilterAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("Table C")),
                            true)),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Equal(10, results.Count());
                        return true;
                    }),
                },
                new object[]
                {
                    "Exact Match, Link Single Table, Context Requires Fewer Drops Than Table Provides",
                    _filterContextFactory.CreateContext(
                        1,
                        1,
                        new FilterAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("Table C")),
                            true)),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Single(results);
                        return true;
                    }),
                },
                new object[]
                {
                    "Weather Matches",
                    _filterContextFactory.CreateContext(
                        3,
                        3,
                        _filterContextProvider
                            .GetContext()
                            .Attributes
                            .AppendSingle(
                            new FilterAttribute(
                                new StringIdentifier("drop-table"),
                                new IdentifierFilterAttributeValue(new StringIdentifier("Weather Table")),
                                true))),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Equal(3, results.Count());
                        return true;
                    }),
                },
                new object[]
                {
                    "Time of Day Matches",
                    _filterContextFactory.CreateContext(
                        3,
                        3,
                        _filterContextProvider
                            .GetContext()
                            .Attributes
                            .AppendSingle(
                            new FilterAttribute(
                                new StringIdentifier("drop-table"),
                                new IdentifierFilterAttributeValue(new StringIdentifier("Time of Day Table")),
                                true))),
                    new Predicate<IEnumerable<IGameObject>>(results =>
                    {
                        Assert.Equal(3, results.Count());
                        return true;
                    }),
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class TestDataModule : SingleRegistrationModule
        {
            protected override void SafeLoad(ContainerBuilder builder)
            {
                builder
                    .Register(x =>
                    {
                        var weatherManager = x.Resolve<IReadOnlyWeatherManager>();
                        var timeOfDayManager = x.Resolve<IReadOnlyTimeOfDayManager>();
                        return new InMemoryDropTableRepository(new IDropTable[]
                        {
                        // Match NOTHING Table, Generates Exactly 3
                        new ItemDropTable(
                            new StringIdentifier("Not something you will match on"),
                            3,
                            3),
                        // Match Specific Id, Generates Exactly 3
                        new ItemDropTable(
                            new StringIdentifier("Table B"),
                            3,
                            3),
                        // LinkedTo Specific, Still Generates
                        new LinkedDropTable(
                            new StringIdentifier("Table C"),
                            2,
                            2,
                            new IWeightedEntry[]
                            {
                                new WightedEntry(1, new StringIdentifier("Table B")),
                            }),
                        // Weather Table, Generates 1
                        new ItemDropTable(
                            new StringIdentifier("Weather Table"),
                            1,
                            1,
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("weather"),
                                    new IdentifierFilterAttributeValue(weatherManager.WeatherId),
                                    true),
                            },
                            Enumerable.Empty<IFilterAttribute>()),
                        // Time of Day Table, Generates 1
                        new ItemDropTable(
                            new StringIdentifier("Time of Day Table"),
                            1,
                            1,
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("time-of-day"),
                                    new IdentifierFilterAttributeValue(timeOfDayManager.TimeOfDay),
                                    true),
                            },
                            Enumerable.Empty<IFilterAttribute>()),
                        // Actor Stat, Generates 1
                        new ItemDropTable(
                            new StringIdentifier("Actor Stats Table"),
                            1,
                            1,
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("actor-stats"),
                                    new ActorStatFilterAttributeValue(
                                        new StringIdentifier("player"),
                                        new StringIdentifier("STAT_A"),
                                        0,
                                        100),
                                    true),
                            },
                            Enumerable.Empty<IFilterAttribute>()),
                        });
                    })
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
                public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = Enumerable.Empty<IFilterAttribute>();

                public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
                {
                    yield return new Mock<IGameObject>(MockBehavior.Strict).Object;
                }
            }
        }
    }
}
