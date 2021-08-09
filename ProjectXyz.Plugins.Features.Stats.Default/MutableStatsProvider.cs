using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            _stats.Changed += (s, e) => StatModified?.Invoke(this, e);
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats => _stats;

        public event EventHandler<StatChangedEventArgs> StatModified;

        public void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback)
        {
            callback(_stats);
        }

        public sealed class StatModifiedDictionary :
            IDictionary<IIdentifier, double>,
            IReadOnlyDictionary<IIdentifier, double>
        {
            private readonly Dictionary<IIdentifier, double> _wrapped;

            public StatModifiedDictionary(IEnumerable<KeyValuePair<IIdentifier, double>> items)
            {
                _wrapped = items.ToDictionary(x => x.Key, x => x.Value);
            }

            public double this[IIdentifier key]
            {
                get => _wrapped[key];
                set
                {
                    var changed =
                        !_wrapped.TryGetValue(key, out var existing) ||
                        existing != value;
                    if (!changed)
                    {
                        return;
                    }

                    _wrapped[key] = value;
                    OnChanged(StatChanged.Changed, key, value);
                }
            }

            public event EventHandler<StatChangedEventArgs> Changed;

            public ICollection<IIdentifier> Keys => _wrapped.Keys;

            public ICollection<double> Values => _wrapped.Values;

            public int Count => _wrapped.Count;

            public bool IsReadOnly => false;

            IEnumerable<IIdentifier> IReadOnlyDictionary<IIdentifier, double>.Keys => Keys;

            IEnumerable<double> IReadOnlyDictionary<IIdentifier, double>.Values => Values;

            public void Add(IIdentifier key, double value)
            {
                _wrapped.Add(key, value);
                OnChanged(StatChanged.Added, key, value);
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

                OnChanged(StatChanged.Removed, key, null);
                return true;
            }

            public bool Remove(KeyValuePair<IIdentifier, double> item) =>
                throw new NotSupportedException();

            public bool TryGetValue(IIdentifier key, out double value) =>
                _wrapped.TryGetValue(key, out value);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private void OnChanged(
                StatChanged status,
                IIdentifier statDefinitionId,
                double? value) =>
                Changed?.Invoke(
                    this,
                    new StatChangedEventArgs(
                        status,
                        statDefinitionId,
                        value));
        }
    }
}