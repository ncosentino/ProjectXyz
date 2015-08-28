using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Data.Tests.Unit.Stats.Mocks;
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
            var stat = new MockStatBuilder().Build();
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
                new MockStatBuilder().WithStatId(new Guid("b0a7f872-07ef-48f9-a29f-126738f1d26f")).Build(),
                new MockStatBuilder().WithStatId(new Guid("577c7455-4a59-4589-9c45-ee7aa6e4bfd0")).Build(),
                new MockStatBuilder().WithStatId(new Guid("31a039fe-bf9f-462b-b5a3-3e176c98a42f")).Build(),
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
            var stats = StatCollection.Create();

            var statsToAdd = new IStat[]
            {
                new MockStatBuilder().WithStatId(new Guid("577c7455-4a59-4589-9c45-ee7aa6e4bfd0")).Build(),
                new MockStatBuilder().WithStatId(new Guid("577c7455-4a59-4589-9c45-ee7aa6e4bfd0")).Build(),
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
            var stat = new MockStatBuilder().Build();
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
            var stat = new MockStatBuilder().Build();
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
                new MockStatBuilder().WithStatId(new Guid("b0a7f872-07ef-48f9-a29f-126738f1d26f")).Build(),
                new MockStatBuilder().WithStatId(new Guid("577c7455-4a59-4589-9c45-ee7aa6e4bfd0")).Build(),
                new MockStatBuilder().WithStatId(new Guid("31a039fe-bf9f-462b-b5a3-3e176c98a42f")).Build(),
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
