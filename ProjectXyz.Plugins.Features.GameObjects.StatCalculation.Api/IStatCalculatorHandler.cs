namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IStatCalculatorHandler
    {
        CalculateStatDelegate CalculateStat { get; }

        CanCalculateStatDelegate CanCalculateStat { get; }
    }
}
