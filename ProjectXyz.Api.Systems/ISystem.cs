using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.Systems
{
    public interface ISystem
    {
        void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors);
    }
}