using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default
{
    public sealed class MutableStatsProvider : IMutableStatsProvider
    {
        private readonly StatModifiedDictionary _stats;

        public MutableStatsProvider()
            : this(new Dictionary<IIdentifier, double>())
        {
        }

        public MutableStatsProvider(IEnumerable<KeyValuePair<IIdentifier, double>> stats)
        {
            _stats = new StatModifiedDictionary(stats);
            _stats.StatsChanged += async (s, e) => await StatsModified
                .InvokeOrderedAsync(this, e)
                .ConfigureAwait(false);
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats => _stats;

        public event EventHandler<StatsChangedEventArgs> StatsModified;

        public Task UsingMutableStatsAsync(Func<IDictionary<IIdentifier, double>, Task> callback) => _stats.BulkUpdateAsync(async () =>
        {
            await callback(_stats).ConfigureAwait(false);
        });

        public sealed class StatModifiedDictionary :
            IDictionary<IIdentifier, double>,
            IReadOnlyDictionary<IIdentifier, double>
        {
            private readonly Dictionary<IIdentifier, double> _wrapped;
            private bool _performingBulkUpdate;

            public StatModifiedDictionary(IEnumerable<KeyValuePair<IIdentifier, double>> items)
            {
                _wrapped = items.ToDictionary(x => x.Key, x => x.Value);
            }

            public double this[IIdentifier key]
            {
                get => _wrapped.TryGetValue(key, out var existingValue)
                    ? existingValue
                    : default;
                set
                {
                    var hadKey = _wrapped.TryGetValue(key, out var existingValue);
                    var changed =
                        !hadKey ||
                        existingValue != value;
                    if (!changed)
                    {
                        return;
                    }

                    _wrapped[key] = value;
                    OnChangedAsync(
                        hadKey 
                            ? Features.Stats.StatChanged.Changed
                            : Features.Stats.StatChanged.Added,
                        key,
                        value, 
                        existingValue).Wait();
                }
            }

            public event EventHandler<StatsChangedEventArgs> StatsChanged;

            private event EventHandler<StatChangedEventArgs> StatChanged;

            private event EventHandler<StatsChangedEventArgs> BulkStatsChangedCollector;

            public ICollection<IIdentifier> Keys => _wrapped.Keys;

            public ICollection<double> Values => _wrapped.Values;

            public int Count => _wrapped.Count;

            public bool IsReadOnly => false;

            IEnumerable<IIdentifier> IReadOnlyDictionary<IIdentifier, double>.Keys => Keys;

            IEnumerable<double> IReadOnlyDictionary<IIdentifier, double>.Values => Values;

            public async Task BulkUpdateAsync(Func<Task> callback)
            {
                // FIXME: this pattern makes me SOOOO nervous. do we need to
                // enforce mutual exclusion here across threads w/ a lock?
                // this is super dangerous otherwise.
                var wasPerformingBulkUpdate = _performingBulkUpdate;
                _performingBulkUpdate = true;
                try
                {
                    var added = new Dictionary<IIdentifier, double>();
                    var removed = new HashSet<IIdentifier>();
                    var changed = new Dictionary<IIdentifier, Tuple<double, double>>();
                    EventHandler<StatChangedEventArgs> handler = (s, e) =>
                    {
                        if (e.Status == Features.Stats.StatChanged.Added)
                        {
                            added[e.StatDefinitionId] = e.Value.Value;
                        }
                        else if (e.Status == Features.Stats.StatChanged.Removed)
                        {
                            removed.Add(e.StatDefinitionId);
                        }
                        else
                        {
                            changed[e.StatDefinitionId] = Tuple.Create(
                                e.Value.Value,
                                e.OldValue.Value);
                        }
                    };

                    StatChanged += handler;
                    try
                    {
                        await callback
                            .Invoke()
                            .ConfigureAwait(false);
                    }
                    finally
                    {
                        StatChanged -= handler;
                    }


                    if (added.Any() || removed.Any() || changed.Any())
                    {
                        var statsChangedArgs = new StatsChangedEventArgs(added, removed, changed);

                        // this gets SUPER messy here but here's the
                        // explanation... the event handlers for stats being
                        // changed can cause other stats to get changed,
                        // triggering this sort of recursive stat-changing
                        // chain. a good example of this is if experience
                        // requirements are met, we want to increase the level
                        // of an actor. therefore, it's one stat change
                        // triggering another. in order to help preserve the
                        // ordering that callers would expect these events to
                        // complete, we need to make this logic feel like it's
                        // completing the first event handler before moving on
                        // to sub-event handlers.
                        if (wasPerformingBulkUpdate)
                        {
                            await BulkStatsChangedCollector
                                .InvokeOrderedAsync(this, statsChangedArgs)
                                .ConfigureAwait(false);
                        }
                        else
                        {
                            var changedArgsAccumulator = new Queue<StatsChangedEventArgs>();
                            changedArgsAccumulator.Enqueue(statsChangedArgs);

                            EventHandler<StatsChangedEventArgs> bulkCollectorHandler = (s, e) => changedArgsAccumulator.Enqueue(e);
                            BulkStatsChangedCollector += bulkCollectorHandler;
                            try
                            {
                                while (changedArgsAccumulator.Count > 0)
                                {
                                    await StatsChanged
                                        .InvokeOrderedAsync(
                                            this,
                                            changedArgsAccumulator.Dequeue())
                                        .ConfigureAwait(false);
                                }
                            }
                            finally
                            {
                                BulkStatsChangedCollector -= bulkCollectorHandler;
                            }
                        }
                    }
                }
                finally
                {
                    _performingBulkUpdate = wasPerformingBulkUpdate;
                }
            }

            public void Add(IIdentifier key, double value)
            {
                _wrapped.Add(key, value);
                OnChangedAsync(Features.Stats.StatChanged.Added, key, value, null).Wait();
            }

            public void Add(KeyValuePair<IIdentifier, double> item) =>
                Add(item.Key, item.Value);

            public void Clear() => throw new NotSupportedException();

            public bool Contains(KeyValuePair<IIdentifier, double> item) =>
                _wrapped.Contains(item);

            public bool ContainsKey(IIdentifier key) =>
                _wrapped.ContainsKey(key);

            public void CopyTo(KeyValuePair<IIdentifier, double>[] array, int arrayIndex) =>
                throw new NotSupportedException();

            public IEnumerator<KeyValuePair<IIdentifier, double>> GetEnumerator() =>
                _wrapped.GetEnumerator();

            public bool Remove(IIdentifier key)
            {
                if (!_wrapped.TryGetValue(key, out var existing))
                {
                    return false;
                }

                if (!_wrapped.Remove(key))
                {
                    return false;
                }

                OnChangedAsync(Features.Stats.StatChanged.Removed, key, null, existing).Wait();
                return true;
            }

            public bool Remove(KeyValuePair<IIdentifier, double> item) =>
                throw new NotSupportedException();

            public bool TryGetValue(IIdentifier key, out double value) =>
                _wrapped.TryGetValue(key, out value);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private async Task OnChangedAsync(
                StatChanged status,
                IIdentifier statDefinitionId,
                double? value,
                double? oldValue) =>
                await StatChanged
                    .InvokeOrderedAsync(
                        this,
                        new StatChangedEventArgs(
                            status,
                            statDefinitionId,
                            value,
                            oldValue))
                     .ConfigureAwait(false);
        }
    }
}