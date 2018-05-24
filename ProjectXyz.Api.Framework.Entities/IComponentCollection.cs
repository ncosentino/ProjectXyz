using System.Collections.Generic;

namespace ProjectXyz.Api.Framework.Entities
{
    public interface IComponentCollection : IReadOnlyCollection<IComponent>
    {
        IEnumerable<TComponent> Get<TComponent>()
            where TComponent : IComponent;
    }
}