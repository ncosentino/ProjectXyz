using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatDefinitionToCalculationMapping
    {
        IIdentifier StateDefinitionId { get; }

        string Calculation { get; }
    }
}