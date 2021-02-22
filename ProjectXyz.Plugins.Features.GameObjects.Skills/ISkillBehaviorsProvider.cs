using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors);
    }
}
