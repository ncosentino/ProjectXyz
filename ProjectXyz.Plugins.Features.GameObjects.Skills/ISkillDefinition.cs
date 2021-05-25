using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinition : IHasFilterAttributes
    {
        IEnumerable<IGeneratorComponent> FilterComponents { get; }
    }
}
