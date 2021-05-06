﻿using System.IO;
using System.Text;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Behaviors.Serialization.Newtonsoft
{
    public sealed class HasMutableStatsBehaviorSerializerTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;
        private static readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        static HasMutableStatsBehaviorSerializerTests()
        {
            _serializer = CachedDependencyLoader.LifeTimeScope.Resolve<ISerializer>();
            _deserializer = CachedDependencyLoader.LifeTimeScope.Resolve<IDeserializer>();
            _hasMutableStatsBehaviorFactory = CachedDependencyLoader.LifeTimeScope.Resolve<IHasMutableStatsBehaviorFactory>();
        }

        [Fact]
        private void FullSerialize_MixedStatIdentifierTypes_EquivalentDeserialized()
        {
            var hasMutableStatsBehavior = _hasMutableStatsBehaviorFactory.Create();
            hasMutableStatsBehavior.MutateStats(stats =>
            {
                stats[new StringIdentifier("Stat A")] = 12.3;
                stats[new StringIdentifier("Stat B")] = 45.6;
                stats[new IntIdentifier(111)] = 56.7;
                stats[new IntIdentifier(222)] = 89.0;
            });

            IHasMutableStatsBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    hasMutableStatsBehavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<IHasMutableStatsBehavior>(stream);
            }

            Assert.True(
                result.BaseStats.ContainsKey(new StringIdentifier("Stat A")),
                $"Expecting stats to contain '{new StringIdentifier("Stat A")}'.");
            Assert.Equal(
                12.3,
                result.BaseStats[new StringIdentifier("Stat A")]);
            Assert.True(
                result.BaseStats.ContainsKey(new StringIdentifier("Stat B")),
                $"Expecting stats to contain '{new StringIdentifier("Stat B")}'.");
            Assert.Equal(
                45.6,
                result.BaseStats[new StringIdentifier("Stat B")]);
            Assert.True(
                result.BaseStats.ContainsKey(new IntIdentifier(111)),
                $"Expecting stats to contain '{new IntIdentifier(111)}'.");
            Assert.Equal(
                56.7,
                result.BaseStats[new IntIdentifier(111)]);
            Assert.True(
                result.BaseStats.ContainsKey(new IntIdentifier(222)),
                $"Expecting stats to contain '{new IntIdentifier(222)}'.");
            Assert.Equal(
                89.0,
                result.BaseStats[new IntIdentifier(222)]);
        }
    }
}
