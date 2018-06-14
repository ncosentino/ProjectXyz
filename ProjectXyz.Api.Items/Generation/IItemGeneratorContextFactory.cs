namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemGeneratorContextFactory
    {
        IItemGeneratorContext CreateItemGeneratorContext(
            int minimumCount,
            int maximumCount);
    }
}