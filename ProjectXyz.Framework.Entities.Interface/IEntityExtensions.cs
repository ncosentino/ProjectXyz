using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Framework.Entities.Extensions
{
    public static class IEntityExtensions
    {
        public static TComponent GetFirst<TComponent>(this IEntity entity)
            where TComponent : IComponent
        {
            return entity.Components.GetFirst<TComponent>();
        }

        public static bool Has<TComponent>(this IEntity entity)
            where TComponent : IComponent
        {
            return entity.Components.Has<TComponent>();
        }
    }
}