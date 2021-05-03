using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillRequirementsBehavior : IBehavior
    {
        IReadOnlyCollection<IFilterAttribute> Requirements { get; }
    }
}
