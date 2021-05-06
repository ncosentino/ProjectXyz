using System.IO;
using System.Linq;
using System.Text;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Behaviors.Serialization.Newtonsoft
{
    public sealed class HasEnchantmentsBehaviorSerializerTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;
        private static readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private static readonly IGameObjectFactory _gameObjectFactory;

        static HasEnchantmentsBehaviorSerializerTests()
        {
            _serializer = CachedDependencyLoader.LifeTimeScope.Resolve<ISerializer>();
            _deserializer = CachedDependencyLoader.LifeTimeScope.Resolve<IDeserializer>();
            _hasEnchantmentsBehaviorFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            _gameObjectFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IGameObjectFactory>();
        }

        [Fact]
        private void FullSerialize_TwoEnchantments_EquivalentDeserialized()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            hasEnchantmentsBehavior.AddEnchantments(new[]
            {
                _gameObjectFactory.Create(new [] { new IdentifierBehavior(new StringIdentifier("enchantment1")) }),
                _gameObjectFactory.Create(new [] { new IdentifierBehavior(new StringIdentifier("enchantment2")) }),
            });

            IHasEnchantmentsBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    hasEnchantmentsBehavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<IHasEnchantmentsBehavior>(stream);
            }

            Assert.Equal(2, result.Enchantments.Count);
            Assert.Equal(
                new StringIdentifier("enchantment1"),
                result.Enchantments.First().GetOnly<IIdentifierBehavior>().Id);
            Assert.Equal(
                new StringIdentifier("enchantment2"),
                result.Enchantments.Last().GetOnly<IIdentifierBehavior>().Id);
        }
    }
}
