using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISkillEffectDefinition : IHasFilterAttributes
    {
        IEnumerable<IGeneratorComponent> FilterComponents { get; }
    }
}
