namespace ProjectXyz.Api.Behaviors.Filtering
{
    public interface IDiscoverablePredicateFilterComponentToBehaviorConverter : IFilterComponentToBehaviorConverter
    {
        bool CanConvert(IFilterComponent filterComponent);
    }
}