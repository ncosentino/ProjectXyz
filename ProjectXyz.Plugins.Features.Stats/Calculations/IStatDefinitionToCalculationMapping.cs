using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Calculations
{
    public interface IStatDefinitionToCalculationMapping
    {
        IIdentifier StatDefinitionId { get; }

        string Calculation { get; }
    }
}