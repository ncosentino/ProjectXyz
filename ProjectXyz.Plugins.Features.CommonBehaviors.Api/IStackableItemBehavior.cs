namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IStackableItemBehavior : IReadOnlyStackableItemBehavior
    {
        new int Count { get; set; }
    }
}