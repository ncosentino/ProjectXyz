using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IParallelSkillExecutorBehavior : ISkillExecutorBehavior
    {
    }

    public sealed class ParallelSkillExecutorBehavior : 
        BaseBehavior,
        IParallelSkillExecutorBehavior
    {
        public ParallelSkillExecutorBehavior(
            IEnumerable<IIdentifier> skillIdentifiers)
        {
            SkillIdentifiers = skillIdentifiers.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIdentifiers { get; }
    }
}
