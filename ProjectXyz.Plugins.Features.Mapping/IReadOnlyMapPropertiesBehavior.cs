using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IReadOnlyMapPropertiesBehavior : IBehavior
    {
        IReadOnlyDictionary<string, object> Properties { get; }
    }
}