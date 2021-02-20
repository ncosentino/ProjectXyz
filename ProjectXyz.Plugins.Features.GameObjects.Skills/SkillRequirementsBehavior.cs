using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillRequirementsBehavior :
        BaseBehavior,
        ISkillRequirementsBehavior
    {
        public SkillRequirementsBehavior(IEnumerable<IFilterAttribute> requirements)
        {
            Requirements = requirements.ToArray();
        }

        public IReadOnlyCollection<IFilterAttribute> Requirements { get; }
    }
}
