using System.Collections.Generic;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Interface.Systems
{
    public interface ISystem
    {
        void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors);
    }
}