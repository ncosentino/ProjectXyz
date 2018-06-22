using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IItemContainerBehavior : IObservableItemContainerBehavior
    {
        bool CanAddItem(IGameObject gameObject);

        bool TryAddItem(IGameObject gameObject);

        bool CanRemoveItem(IGameObject gameObject);

        bool TryRemoveItem(IGameObject gameObject);
    }
}
