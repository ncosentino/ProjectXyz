using System.Collections.Generic;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework.Entities;

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