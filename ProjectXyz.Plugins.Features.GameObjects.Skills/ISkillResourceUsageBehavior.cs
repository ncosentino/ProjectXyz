using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillResourceUsageBehavior : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> StaticStatRequirements { get; }
    }
}
