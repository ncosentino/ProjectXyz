using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class HasSkillsGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(HasSkillsBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var hasSkillsBehavior = (IHasSkillsBehavior)behavior;
            foreach (var skill in hasSkillsBehavior.Skills)
            {
                yield return skill;
            }
        }
    }
}
