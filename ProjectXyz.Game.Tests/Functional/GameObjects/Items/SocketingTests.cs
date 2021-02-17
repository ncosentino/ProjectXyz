using System;
using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Items
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
        private void Socket_MatchesSocketPattern_SocketsSuccessfullyAndNewItemCreated()
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
        private void Socket_DoesNotMatchSocketPattern_SocketsSuccessfullyNoNewItem()
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
    }


    public sealed class TestModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestSocketPatternHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public sealed class TestSocketPatternItem : IGameObject
        {
            public IBehaviorCollection Behaviors { get; set; }
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
