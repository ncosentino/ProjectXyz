using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillBehaviorsInterceptor
    {
        void Intercept(IReadOnlyCollection<IBehavior> behaviors);
    }
}
