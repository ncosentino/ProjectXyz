using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Framework.Entities.Interface
{
    public interface IComponentCollection : IReadOnlyCollection<IComponent>
    {
        IEnumerable<TComponent> Get<TComponent>()
            where TComponent : IComponent;
    }

    public static class IComponentCollectionExtensions
    {
        public static TComponent GetFirst<TComponent>(this IComponentCollection components)
            where TComponent : IComponent
        {
            var component = components
                .Get<TComponent>()
                .FirstOrDefault();
            if (component == null)
            {
                throw new InvalidOperationException($"Could not find a component of type '{typeof(TComponent)}'.");
            }

            return component;
        }
    }
}