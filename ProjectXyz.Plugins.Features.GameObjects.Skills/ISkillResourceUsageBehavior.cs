using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillResourceUsageBehavior : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> StaticStatRequirements { get; }
    }
}
