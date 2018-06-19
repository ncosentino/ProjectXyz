namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorContextFactory
    {
        IGeneratorContext CreateItemGeneratorContext(
            int minimumCount,
            int maximumCount);
    }
}