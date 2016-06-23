namespace ProjectXyz.Application.Interface.Stats
{
    public static class IStatCollectionFactoryExtensions
    {
        public static IStatCollection Create(
            this IStatCollectionFactory statCollectionFactory,
            params IStat[] stats)
        {
            return statCollectionFactory.Create(stats);
        }
    }
}