namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasItemContainersBehavior : IReadOnlyHasItemContainersBehavior
    {
        void AddItemContainer(IItemContainerBehavior itemContainerBehavior);

        bool RemoveItemContainer(IItemContainerBehavior itemContainerBehavior);
    }
}
