using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Game.Interface.Systems;

namespace ProjectXyz.Game.Core.Systems
{
    public sealed class SystemUpdateContext : ISystemUpdateContext
    {
        public SystemUpdateContext(IEnumerable<IComponent> components)
        {
            Components = new ComponentCollection(components);
        }

        public IComponentCollection Components { get; }
    
    }
}