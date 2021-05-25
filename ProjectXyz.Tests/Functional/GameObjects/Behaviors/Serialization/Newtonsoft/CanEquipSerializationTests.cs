using System.IO;
using System.Linq;
using System.Text;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Tests.Functional.GameObjects.Behaviors.Serialization.Newtonsoft
{
    public sealed class CanEquipSerializationTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;
        private static readonly IGameObjectFactory _gameObjectFactory;

        static CanEquipSerializationTests()
        {
            _serializer = CachedDependencyLoader.LifeTimeScope.Resolve<ISerializer>();
            _deserializer = CachedDependencyLoader.LifeTimeScope.Resolve<IDeserializer>();
            _gameObjectFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IGameObjectFactory>();
        }

        [Fact]
        private void FullSerialize_EmptyEquipment_EquivalentDeserialized()
        {
            var canEquipBehavior = new CanEquipBehavior(new[]
            {
                new StringIdentifier("head"),
                new StringIdentifier("body"),
                new StringIdentifier("lefthand"),
                new StringIdentifier("righthand"),
            });

            ICanEquipBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    canEquipBehavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<ICanEquipBehavior>(stream);
            }

            Assert.Equal(4, result.SupportedEquipSlotIds.Count);
            Assert.Contains(new StringIdentifier("head"), result.SupportedEquipSlotIds);
            Assert.Contains(new StringIdentifier("body"), result.SupportedEquipSlotIds);
            Assert.Contains(new StringIdentifier("lefthand"), result.SupportedEquipSlotIds);
            Assert.Contains(new StringIdentifier("righthand"), result.SupportedEquipSlotIds);
            Assert.Empty(result.GetEquippedItems());
        }

        [Fact]
        private void FullSerialize_MultipleEquippedItems_EquivalentDeserialized()
        {
            var helmet = _gameObjectFactory.Create(new IBehavior[]
            {
                new IdentifierBehavior(new StringIdentifier("helmet")),
                new CanBeEquippedBehavior(new StringIdentifier("head")),
            });
            var armor = _gameObjectFactory.Create(new IBehavior[]
            {
                new IdentifierBehavior(new StringIdentifier("armor")),
                new CanBeEquippedBehavior(new StringIdentifier("body")),
            });

            var canEquipBehavior = new CanEquipBehavior(new[]
            {
                new StringIdentifier("head"),
                new StringIdentifier("body"),
                new StringIdentifier("lefthand"),
                new StringIdentifier("righthand"),
            });
            canEquipBehavior.TryEquip(new StringIdentifier("head"), helmet.GetOnly<ICanBeEquippedBehavior>(), false);
            canEquipBehavior.TryEquip(new StringIdentifier("body"), armor.GetOnly<ICanBeEquippedBehavior>(), false);

            ICanEquipBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    canEquipBehavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<ICanEquipBehavior>(stream);
            }

            Assert.Equal(4, result.SupportedEquipSlotIds.Count);
            Assert.Contains(new StringIdentifier("head"), result.SupportedEquipSlotIds);
            Assert.Contains(new StringIdentifier("body"), result.SupportedEquipSlotIds);
            Assert.Contains(new StringIdentifier("lefthand"), result.SupportedEquipSlotIds);
            Assert.Contains(new StringIdentifier("righthand"), result.SupportedEquipSlotIds);

            var equippedItemsBySlot = result
                .GetEquippedItemsBySlot()
                .ToDictionary(
                    x => x.Item1,
                    x => x.Item2);
            Assert.Equal(2, equippedItemsBySlot.Count);
            Assert.Equal(
                new StringIdentifier("helmet"),
                equippedItemsBySlot[new StringIdentifier("head")].GetOnly<IIdentifierBehavior>().Id);
            Assert.Equal(
                new StringIdentifier("armor"),
                equippedItemsBySlot[new StringIdentifier("body")].GetOnly<IIdentifierBehavior>().Id);
        }
    }
}
