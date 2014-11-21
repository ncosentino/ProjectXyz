using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;

using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Data.Tests.Stats.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Data.Tests.Stats
{
    [DataLayer]
    [Stats]
    public class StatCollectionTests
    {
        [Fact]
        public void StatCollection_AddSingle_HasItem()
        {
            var stat = new MockStatBuilder().Build();
            var stats = StatCollection.Create();

            stats.Add(stat);

            Assert.Equal(1, stats.Count);
            Assert.Contains(stat, stats);
        }

        [Fact]
        public void StatCollection_AddMultipleUnique_HasItems()
        {
            var stats = StatCollection.Create();

            var statsToAdd = new IStat[]
            {
                new MockStatBuilder().WithStatId("A").Build(),
                new MockStatBuilder().WithStatId("B").Build(),
                new MockStatBuilder().WithStatId("C").Build(),
            };

            stats.Add(statsToAdd);

            Assert.Equal(statsToAdd.Length, stats.Count);

            for (int i = 0; i < statsToAdd.Length; ++i)
            {
                Assert.True(
                    stats.Contains(statsToAdd[i]),
                    "Expected stat " + i + " would be contained in collection");
            }
        }

        [Fact]
        public void StatCollection_AddMultipleDuplicates_Throws()
        {
            var stats = StatCollection.Create();

            var statsToAdd = new IStat[]
            {
                new MockStatBuilder().WithStatId("A").Build(),
                new MockStatBuilder().WithStatId("A").Build(),
            };

            Assert.Throws<ArgumentException>(() => stats.Add(statsToAdd));
        }

        [Fact]
        public void StatCollection_RemoveExistingSingle_Successful()
        {
            var stat = new MockStatBuilder().Build();
            var stats = StatCollection.Create();
            stats.Add(stat);

            Assert.True(
                stats.Remove(stat),
                "Expected to remove stat.");
            Assert.Empty(stats);
        }

        [Fact]
        public void StatCollection_RemoveNonexistentSingle_Fails()
        {
            var stat = new MockStatBuilder().Build();
            var stats = StatCollection.Create();

            Assert.False(
                stats.Remove(stat),
                "Expected to NOT remove stat.");
            Assert.Empty(stats);
        }

        [Fact]
        public void StatCollection_RemoveMultiple_Successful()
        {
            var stats = StatCollection.Create();

            var statsToRemove = new IStat[]
            {
                new MockStatBuilder().WithStatId("A").Build(),
                new MockStatBuilder().WithStatId("B").Build(),
                new MockStatBuilder().WithStatId("C").Build(),
            };

            stats.Add(statsToRemove);
            
            Assert.True(
                stats.Remove(statsToRemove),
                "Expected to remove stats.");
            Assert.Empty(stats);
        }
    }
}
