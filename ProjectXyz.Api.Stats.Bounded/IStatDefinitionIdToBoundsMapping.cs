using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMapping
    {
        IIdentifier StatDefinitiondId { get; }

        IStatBounds StatBounds { get; }
    }
}
