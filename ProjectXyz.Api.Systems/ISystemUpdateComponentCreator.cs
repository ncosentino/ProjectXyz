using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Systems
{
    public interface ISystemUpdateComponentCreator
    {
        IEnumerable<IComponent> CreateNext(IReadOnlyCollection<IComponent> currentComponents);
    }
}
