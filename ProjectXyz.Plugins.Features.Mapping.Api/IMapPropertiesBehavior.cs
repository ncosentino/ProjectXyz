using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapPropertiesBehavior : IReadOnlyMapPropertiesBehavior
    {
        new IDictionary<string, object> Properties { get; }
    }
}