using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IHasSkillSynergiesBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> SkillSynergies { get; }
    }
}
