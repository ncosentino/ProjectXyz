using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillResourceUsageBehavior :
        BaseBehavior,
        ISkillResourceUsageBehavior
    {
        public SkillResourceUsageBehavior(IEnumerable<KeyValuePair<IIdentifier, double>> staticStatRequirements)
        {
            StaticStatRequirements = staticStatRequirements.ToDictionary();
        }

        public IReadOnlyDictionary<IIdentifier, double> StaticStatRequirements { get; }
    }
}
