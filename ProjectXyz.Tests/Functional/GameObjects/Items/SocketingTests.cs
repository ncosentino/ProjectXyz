using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Tests.Functional.GameObjects.Items
{
    public sealed class SocketingTests
    {
        private static IItemFactory _itemFactory;
        private static ISocketPatternHandlerFacade _socketPatternHandlerFacade;
        private static ISocketableInfoFactory _socketableInfoFactory;
        private static ICanBeSocketedBehaviorFactory _canBeSocketedBehaviorFactory;
        private static ICanFitSocketBehaviorFactory _canFitSocketBehaviorFactory;

        static SocketingTests()
        {
            _itemFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IItemFactory>();
            _socketPatternHandlerFacade = CachedDependencyLoader.LifeTimeScope.Resolve<ISocketPatternHandlerFacade>();
            _socketableInfoFactory = CachedDependencyLoader.LifeTimeScope.Resolve<ISocketableInfoFactory>();
            _canBeSocketedBehaviorFactory = CachedDependencyLoader.LifeTimeScope.Resolve<ICanBeSocketedBehaviorFactory>();
            _canFitSocketBehaviorFactory = CachedDependencyLoader.LifeTimeScope.Resolve<ICanFitSocketBehaviorFactory>();
        }

        [Fact]
        private void TryHandle_MatchesSocketPattern_SocketsSuccessfullyAndNewItemCreated()
        {
            var socketTypeId = new StringIdentifier("socket type id");

            var canBeSocketedItem = _itemFactory.Create(
                _canBeSocketedBehaviorFactory.Create(new[]
                {
                    socketTypeId,
                }));

            var canFitSocketBehavior = _canFitSocketBehaviorFactory.Create(
                socketTypeId,
                1);
            var canFitSocketItem = _itemFactory.Create(canFitSocketBehavior);

            Assert.True(
                canBeSocketedItem
                    .GetOnly<ICanBeSocketedBehavior>()
                    .Socket(canFitSocketItem.GetOnly<ICanFitSocketBehavior>()),
                $"Expected to be able socket '{canFitSocketItem}' into " +
                $"'{canBeSocketedItem}'.");

            Assert.True(_socketPatternHandlerFacade.TryHandle(
                _socketableInfoFactory.Create(canBeSocketedItem),
                out var socketPatternItem));
            Assert.NotNull(socketPatternItem);
            Assert.IsType<TestModule.TestSocketPatternItem>(socketPatternItem);
        }

        [Fact]
        private void TryHandle_DoesNotMatchSocketPattern_SocketsSuccessfullyNoNewItem()
        {
            var socketTypeId = new StringIdentifier(Guid.NewGuid().ToString());

            var canBeSocketedItem = _itemFactory.Create(
                _canBeSocketedBehaviorFactory.Create(new[]
                {
                    socketTypeId,
                }));

            var canFitSocketBehavior = _canFitSocketBehaviorFactory.Create(
                socketTypeId,
                1);
            var canFitSocketItem = _itemFactory.Create(canFitSocketBehavior);

            Assert.True(
                canBeSocketedItem
                    .GetOnly<ICanBeSocketedBehavior>()
                    .Socket(canFitSocketItem.GetOnly<ICanFitSocketBehavior>()),
                $"Expected to be able socket '{canFitSocketItem}' into " +
                $"'{canBeSocketedItem}'.");

            Assert.False(_socketPatternHandlerFacade.TryHandle(
                _socketableInfoFactory.Create(canBeSocketedItem),
                out var socketPatternItem));
            Assert.Null(socketPatternItem);
        }

        [Fact]
        private void TryHandle_MeetsTransformativeDefinition_SuccessAndNewItemReturned()
        {
            var socketTypeId = new StringIdentifier(Guid.NewGuid().ToString());

            var canBeSocketedItem = _itemFactory.Create(
                _canBeSocketedBehaviorFactory.Create(new[]
                {
                    socketTypeId,
                }),
                new TagsBehavior(new StringIdentifier("target socketable base item")));

            var canFitSocketBehavior = _canFitSocketBehaviorFactory.Create(
                socketTypeId,
                1);
            var canFitSocketItem = _itemFactory.Create(
                canFitSocketBehavior,
                new TagsBehavior(new StringIdentifier("socket 0 tag")));

            Assert.True(
                canBeSocketedItem
                    .GetOnly<ICanBeSocketedBehavior>()
                    .Socket(canFitSocketItem.GetOnly<ICanFitSocketBehavior>()),
                $"Expected to be able socket '{canFitSocketItem}' into " +
                $"'{canBeSocketedItem}'.");

            Assert.True(_socketPatternHandlerFacade.TryHandle(
                _socketableInfoFactory.Create(canBeSocketedItem),
                out var socketPatternItem));
            Assert.NotNull(socketPatternItem);
            Assert.NotEqual(canBeSocketedItem, socketPatternItem);
        }
    }

    public sealed class TestModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestSocketPatternHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(x => new InMemoryTransformativeSocketPatternRepository(new ITransformativeSocketPatternDefinition[]
                {
                    new TransformativeSocketPatternDefinition(
                        new IFilterAttributeValue[]
                        {
                            new AnyTagsFilter(new StringIdentifier("target socketable base item")),
                            new OrderedSocketFilter(new[]
                            {
                                // single socket
                                new[]
                                {
                                    new AnyTagsFilter(new StringIdentifier("socket 0 tag")),
                                }
                            }),
                        },
                        new IGeneratorComponent[] { }),
                }))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public sealed class TestSocketPatternItem : IGameObject
        {
            public IReadOnlyCollection<IBehavior> Behaviors { get; set; }
        }

        private sealed class TestSocketPatternHandler : IDiscoverableSocketPatternHandler
        {
            public bool TryHandle(ISocketableInfo socketableInfo, out IGameObject newItem)
            {
                newItem = null;

                if (socketableInfo.AvailableSocketCount == 0 &&
                    socketableInfo.TotalSocketCount == 1 &&
                    socketableInfo.OccupiedSockets.Single().SocketType.Equals(new StringIdentifier("socket type id")))
                {
                    newItem = new TestSocketPatternItem();
                    return true;
                }

                return false;
            }
        }
    }
}
