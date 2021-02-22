using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class Skill : IGameObject
    {
        public Skill(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
