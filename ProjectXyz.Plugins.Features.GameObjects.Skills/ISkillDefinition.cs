using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinition : IHasFilterAttributes
    {
        IEnumerable<IGeneratorComponent> FilterComponents { get; }
    }
}
