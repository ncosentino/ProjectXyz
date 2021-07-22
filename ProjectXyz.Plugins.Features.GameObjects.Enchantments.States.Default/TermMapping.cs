using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
{
    public sealed class TermMapping : ITermMapping
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _wrapped;

        public TermMapping(IEnumerable<KeyValuePair<IIdentifier, string>> wrapped)
        {
            _wrapped = wrapped.ToDictionary(x => x.Key, x => x.Value);
        }

        public IEnumerator<KeyValuePair<IIdentifier, string>> GetEnumerator() => _wrapped.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _wrapped.Count;

        public bool ContainsKey(IIdentifier key) => _wrapped.ContainsKey(key);

        public bool TryGetValue(IIdentifier key, out string value) => _wrapped.TryGetValue(key, out value);

        public string this[IIdentifier key] => _wrapped[key];

        public IEnumerable<IIdentifier> Keys => _wrapped.Keys;

        public IEnumerable<string> Values => _wrapped.Values;
    }
}
