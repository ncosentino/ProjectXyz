using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.BoundedStats.Api
{
    public interface IStatDefinitionIdToBoundsMapping
    {
        IIdentifier StatDefinitiondId { get; }

        IStatBounds StatBounds { get; }
    }
}
