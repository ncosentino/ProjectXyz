using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillExecutorBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> SkillIdentifiers { get; }
    }

    public interface ICombinationSkillBehavior : IBehavior
    {
        IReadOnlyCollection<ISkillExecutorBehavior> SkillExecutors { get; }
    }

    public sealed class CombinationSkillBehavior : 
        BaseBehavior,
        ICombinationSkillBehavior
    {
        public CombinationSkillBehavior(
            IEnumerable<ISkillExecutorBehavior> skillExecutors)
        {
            SkillExecutors = skillExecutors.ToArray();
        }

        public IReadOnlyCollection<ISkillExecutorBehavior> SkillExecutors { get; }
    }
}
