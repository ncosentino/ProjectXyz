namespace ProjectXyz.Framework.Entities.Interface
{
    public interface IEntity
    {
        IComponentCollection Components { get; }
    }

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