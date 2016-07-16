using System.Collections.Generic;

namespace ProjectXyz.Framework.Entities.Interface
{
    public interface IComponentCollection : IReadOnlyCollection<IComponent>
    {
        IEnumerable<TComponent> Get<TComponent>()
            where TComponent : IComponent;
    }
}