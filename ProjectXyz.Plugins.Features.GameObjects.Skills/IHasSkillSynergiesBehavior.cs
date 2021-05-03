using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IHasSkillSynergiesBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> SkillSynergies { get; }
    }
}
