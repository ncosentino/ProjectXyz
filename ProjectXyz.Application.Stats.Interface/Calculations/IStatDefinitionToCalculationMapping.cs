using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatDefinitionToCalculationMapping
    {
        IIdentifier StatDefinitionId { get; }

        string Calculation { get; }
    }
}