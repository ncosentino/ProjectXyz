using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class HasSkillsBehavior :
        BaseBehavior,
        IHasSkillsBehavior
    {
        private readonly List<IGameObject> _skills;

        public HasSkillsBehavior()
            : this(Enumerable.Empty<IGameObject>())
        {
        }

        public HasSkillsBehavior(IEnumerable<IGameObject> skills)
        {
            _skills = new List<IGameObject>(skills);
        }

        public IReadOnlyCollection<IGameObject> Skills => _skills;

        public void Add(IEnumerable<IGameObject> skills)
        {
            _skills.AddRange(skills);
        }

        public void Remove(IEnumerable<IGameObject> skills)
        {
            foreach (var skill in skills)
            {
                _skills.Remove(skill);
            }
        }
    }
}
