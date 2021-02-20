using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IReadOnlyHasSkillsBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> Skills { get; }
    }
}
