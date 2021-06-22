using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapPropertiesBehavior :
        BaseBehavior,
        IMapPropertiesBehavior
    {
        private readonly Dictionary<string, object> _properties;

        public MapPropertiesBehavior(IEnumerable<KeyValuePair<string, object>> properties)
        {
            _properties = new Dictionary<string, object>();
            foreach (var kvp in properties)
            {
                _properties.Add(kvp.Key, kvp.Value);
            }
        }

        public IDictionary<string, object> Properties => _properties;

        IReadOnlyDictionary<string, object> IReadOnlyMapPropertiesBehavior.Properties => _properties;
    }
}