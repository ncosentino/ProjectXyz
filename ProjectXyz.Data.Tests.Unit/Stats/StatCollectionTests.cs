using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Stats
{
    [DataLayer]
    [Stats]
    public class StatCollectionTests
    {
        [Fact]
        public void Add_Single_HasItem()
        {
            // Setup
            var stat = Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123);
            var stats = StatCollection.Create();

            // Execute
            stats.Add(stat);

            // Assert
            Assert.Equal(1, stats.Count);
            Assert.Contains(stat, stats);
        }

        [Fact]
        public void Add_MultipleUnique_HasItems()
        {
            // Setup
            var stats = StatCollection.Create();

            var statsToAdd = new IStat[]
            {
                Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123),
                Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123),
                Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123),
            };

            // Execute
            stats.Add(statsToAdd);

            // Assert
            Assert.Equal(statsToAdd.Length, stats.Count);

            for (int i = 0; i < statsToAdd.Length; ++i)
            {
                Assert.True(
                    stats.Contains(statsToAdd[i]),
                    "Expected stat " + i + " would be contained in collection");
            }
        }

        [Fact]
        public void Add_MultipleDuplicates_Throws()
        {
            // Setup
            var statDefinitionId = Guid.NewGuid();
            
            var stats = StatCollection.Create();

            var statsToAdd = new IStat[]
            {
                Stat.Create(Guid.NewGuid(), statDefinitionId, 123),
                Stat.Create(Guid.NewGuid(), statDefinitionId, 123),
            };

            // Execute
            Assert.ThrowsDelegate method = () => stats.Add(statsToAdd);

            // Assert
            Assert.Throws<ArgumentException>(method);
        }

        [Fact]
        public void Remove_ExistingSingle_Successful()
        {
            // Setup
            var stat = Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123);
            var stats = StatCollection.Create();

            stats.Add(stat);

            // Execute
            var result = stats.Remove(stat);

            // Assert
            Assert.True(
                result,
                "Expected to remove stat.");
            Assert.Empty(stats);
        }

        [Fact]
        public void Remove_NonexistentSingle_Fails()
        {
            // Setup
            var stat = Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123);
            var stats = StatCollection.Create();

            // Execute
            var result = stats.Remove(stat);

            // Assert
            Assert.False(
                result,
                "Expected to NOT remove stat.");
            Assert.Empty(stats);
        }

        [Fact]
        public void Remove_Multiple_Successful()
        {
            // Setup
            var stats = StatCollection.Create();

            var statsToRemove = new IStat[]
            {
                Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123),
                Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123),
                Stat.Create(Guid.NewGuid(), Guid.NewGuid(), 123),
            };

            stats.Add(statsToRemove);

            // Execute
            var result = stats.Remove(statsToRemove);

            // Assert
            Assert.True(
                result,
                "Expected to remove stats.");
            Assert.Empty(stats);
        }
    }
}
