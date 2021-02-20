using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillRequirementsBehavior : IBehavior
    {
        IReadOnlyCollection<IFilterAttribute> Requirements { get; }
    }
}
