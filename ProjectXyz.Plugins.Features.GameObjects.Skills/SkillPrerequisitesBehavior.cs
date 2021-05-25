using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillPrerequisitesBehavior :
        BaseBehavior,
        ISkillPrerequisitesBehavior
    {
        public SkillPrerequisitesBehavior(IEnumerable<IFilterAttribute> prerequisites)
        {
            Prerequisites = prerequisites.ToArray();
        }

        public IReadOnlyCollection<IFilterAttribute> Prerequisites { get; }
    }
}
