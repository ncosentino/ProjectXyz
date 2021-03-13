namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IDiscoverablePredicateGeneratorComponentToBehaviorConverter : IGeneratorComponentToBehaviorConverter
    {
        bool CanConvert(IGeneratorComponent generatorComponent);
    }
}