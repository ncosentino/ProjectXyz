using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IHasSkillsBehavior : IReadOnlyHasSkillsBehavior
    {
        void Add(IEnumerable<IGameObject> skills);

        void Remove(IEnumerable<IGameObject> skills);
    }
}
