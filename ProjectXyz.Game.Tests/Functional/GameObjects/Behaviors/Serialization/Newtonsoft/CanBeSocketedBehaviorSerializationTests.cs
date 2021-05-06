using System.IO;
using System.Linq;
using System.Text;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Behaviors.Serialization.Newtonsoft
{
    public sealed class CanBeSocketedBehaviorSerializationTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;
        private static readonly ICanBeSocketedBehaviorFactory _canBeSocketedBehaviorFactory;
        private static readonly ICanFitSocketBehaviorFactory _canFitSocketBehaviorFactory;
        private static readonly IGameObjectFactory _gameObjectFactory;

        static CanBeSocketedBehaviorSerializationTests()
        {
            _serializer = CachedDependencyLoader.LifeTimeScope.Resolve<ISerializer>();
            _deserializer = CachedDependencyLoader.LifeTimeScope.Resolve<IDeserializer>();
            _canBeSocketedBehaviorFactory = CachedDependencyLoader.LifeTimeScope.Resolve<ICanBeSocketedBehaviorFactory>();
            _canFitSocketBehaviorFactory = CachedDependencyLoader.LifeTimeScope.Resolve<ICanFitSocketBehaviorFactory>();
            _gameObjectFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IGameObjectFactory>();
        }

        [Fact]
        private void FullSerialize_MultipleSocketTypesEmpty_EquivalentDeserialized()
        {
            var canBeSocketedBehavior = _canBeSocketedBehaviorFactory.Create(new[]
            {
                new StringIdentifier("SocketTypeA"),
                new StringIdentifier("SocketTypeA"),
                new StringIdentifier("SocketTypeA"),
                new StringIdentifier("SocketTypeB"),
                new StringIdentifier("SocketTypeB"),
                new StringIdentifier("SocketTypeC"),
            });

            ICanBeSocketedBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    canBeSocketedBehavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<ICanBeSocketedBehavior>(stream);
            }

            Assert.Equal(3, result.AvailableSockets[new StringIdentifier("SocketTypeA")]);
            Assert.Equal(2, result.AvailableSockets[new StringIdentifier("SocketTypeB")]);
            Assert.Equal(1, result.AvailableSockets[new StringIdentifier("SocketTypeC")]);
            Assert.Empty(result.OccupiedSockets);
        }

        [Fact]
        private void FullSerialize_MultipleSocketedItems_EquivalentDeserialized()
        {
            var itemInSocket1 = _gameObjectFactory.Create(new IBehavior[]
            {
                new IdentifierBehavior(new StringIdentifier("item1")),
                _canFitSocketBehaviorFactory.Create(new StringIdentifier("SocketTypeA"), 1),
            });
            var itemInSocket2 = _gameObjectFactory.Create(new IBehavior[]
            {
                new IdentifierBehavior(new StringIdentifier("item2")),
                _canFitSocketBehaviorFactory.Create(new StringIdentifier("SocketTypeB"), 1)
            });

            var canBeSocketedBehavior = _canBeSocketedBehaviorFactory.Create(new[]
            {
                new StringIdentifier("SocketTypeA"),
                new StringIdentifier("SocketTypeB"),
            });
            canBeSocketedBehavior.Socket(itemInSocket1.GetOnly<ICanFitSocketBehavior>());
            canBeSocketedBehavior.Socket(itemInSocket2.GetOnly<ICanFitSocketBehavior>());

            ICanBeSocketedBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    canBeSocketedBehavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<ICanBeSocketedBehavior>(stream);
            }

            Assert.Equal(0, result.AvailableSockets[new StringIdentifier("SocketTypeA")]);
            Assert.Equal(0, result.AvailableSockets[new StringIdentifier("SocketTypeB")]);
            Assert.Equal(2, result.OccupiedSockets.Count);
            Assert.Equal(
                new StringIdentifier("item1"),
                result.OccupiedSockets.First().Owner.GetOnly<IIdentifierBehavior>().Id);
            Assert.Equal(
                new StringIdentifier("item2"),
                result.OccupiedSockets.Last().Owner.GetOnly<IIdentifierBehavior>().Id);
        }
    }
}
