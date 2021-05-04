using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISequentialSkillExecutorBehavior : ISkillExecutorBehavior
    {
    }

    public sealed class SequentialSkillExecutorBehavior : 
        BaseBehavior,
        ISequentialSkillExecutorBehavior
    {
        public SequentialSkillExecutorBehavior(
            IEnumerable<IIdentifier> skillIdentifiers)
        {
            SkillIdentifiers = skillIdentifiers.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIdentifiers { get; }
    }
}
