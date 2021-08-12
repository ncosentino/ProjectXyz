using System;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace ProjectXyz.Plugins.Features.Stats.Tests
{
    public sealed class MutableStatsProviderTests
    {
        private readonly MutableStatsProvider _mutableStatsProvider;

        public MutableStatsProviderTests()
        {
            _mutableStatsProvider = new MutableStatsProvider();
        }

        [Fact]
        private async Task UsingMutableStatsAsync_SetNewStat_StatsModifiedEventWithAddedStat()
        {
            var statsModifiedCount = 0;
            _mutableStatsProvider.StatsModified += (s, e) =>
            {
                Assert.Equal(1, e.AddedStats.Count);
                Assert.Equal(new StringIdentifier("new"), e.AddedStats.Single().Key);
                Assert.Equal(123, e.AddedStats.Single().Value);
                Assert.Empty(e.ChangedStats);
                Assert.Empty(e.RemovedStats);
                statsModifiedCount++;
            };

            await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
            {
                stats[new StringIdentifier("new")] = 123;
            });

            Assert.Equal(1, statsModifiedCount);
        }

        [Fact]
        private async Task UsingMutableStatsAsync_AddNewStat_StatsModifiedEventWithAddedStat()
        {
            var statsModifiedCount = 0;
            _mutableStatsProvider.StatsModified += (s, e) =>
            {
                Assert.Equal(1, e.AddedStats.Count);
                Assert.Equal(new StringIdentifier("new"), e.AddedStats.Single().Key);
                Assert.Equal(123, e.AddedStats.Single().Value);
                Assert.Empty(e.ChangedStats);
                Assert.Empty(e.RemovedStats);
                statsModifiedCount++;
            };

            await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
            {
                stats.Add(new StringIdentifier("new"), 123);
            });

            Assert.Equal(1, statsModifiedCount);
        }

        [Fact]
        private async Task UsingMutableStatsAsync_UpdateExistingStat_StatsModifiedEventWithChangedStat()
        {
            await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
            {
                stats[new StringIdentifier("existing")] = 123;
            });

            var statsModifiedCount = 0;
            _mutableStatsProvider.StatsModified += (s, e) =>
            {
                Assert.Equal(1, e.ChangedStats.Count);
                Assert.Equal(new StringIdentifier("existing"), e.ChangedStats.Single().Key);
                Assert.Equal(456, e.ChangedStats.Single().Value.Item1);
                Assert.Equal(123, e.ChangedStats.Single().Value.Item2);
                Assert.Empty(e.AddedStats);
                Assert.Empty(e.RemovedStats);
                statsModifiedCount++;
            };

            await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
            {
                stats[new StringIdentifier("existing")] = 456;
            });

            Assert.Equal(1, statsModifiedCount);
        }

        [Fact]
        private async Task UsingMutableStatsAsync_IncrementUnsetStat_StatsModifiedEventWithChangedStat()
        {
            var statsModifiedCount = 0;
            _mutableStatsProvider.StatsModified += (s, e) =>
            {
                Assert.Equal(1, e.AddedStats.Count);
                Assert.Equal(new StringIdentifier("unset"), e.AddedStats.Single().Key);
                Assert.Equal(123, e.AddedStats.Single().Value);
                Assert.Empty(e.ChangedStats);
                Assert.Empty(e.RemovedStats);
                statsModifiedCount++;
            };

            await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
            {
                stats[new StringIdentifier("unset")] += 123;
            });

            Assert.Equal(1, statsModifiedCount);
        }

        [Fact]
        private async Task UsingMutableStatsAsync_CascadingHandlers_ExpectedOrdering()
        {
            var firstHandlerEnteredCount = 0;
            var firstHandlerReachedEndCount = 0;
            var secondHandlerEnteredCount = 0;
            var secondHandlerReachedEndCount = 0;

            _mutableStatsProvider.StatsModified += async (s, e) =>
            {
                firstHandlerEnteredCount++;
                if (e.AddedStats.Count != 1 ||
                    !e.AddedStats.Single().Key.Equals(new StringIdentifier("first")))
                {
                    return;
                }

                await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
                {
                    stats[new StringIdentifier("second")] = 456;
                });

                Assert.Equal(0, secondHandlerReachedEndCount);
                firstHandlerReachedEndCount++;
            };
            _mutableStatsProvider.StatsModified += (s, e) =>
            {
                secondHandlerEnteredCount++;
                if (e.AddedStats.Count != 1 ||
                    !e.AddedStats.Single().Key.Equals(new StringIdentifier("second")))
                {
                    return;
                }

                Assert.Equal(1, firstHandlerReachedEndCount);
                secondHandlerReachedEndCount++;
            };

            await _mutableStatsProvider.UsingMutableStatsAsync(async stats =>
            {
                stats[new StringIdentifier("first")] = 123;
            });

            Assert.Equal(2, firstHandlerEnteredCount);
            Assert.Equal(1, firstHandlerReachedEndCount);
            Assert.Equal(2, secondHandlerEnteredCount);
            Assert.Equal(1, secondHandlerReachedEndCount);
        }
    }
}
