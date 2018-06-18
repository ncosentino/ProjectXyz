using System;
using System.Linq;

namespace ProjectXyz.Api.Framework.Entities
{
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

        public static bool Has<TComponent>(this IComponentCollection components)
            where TComponent : IComponent
        {
            var component = components
                .Get<TComponent>()
                .FirstOrDefault();
            return component != null;
        }
    }
}