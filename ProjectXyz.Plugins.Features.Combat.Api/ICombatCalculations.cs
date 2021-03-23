namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public interface ICombatCalculations
    {
        CombatCalculation<double> CalculateActorIncrementValue { get; }

        CombatCalculation<double> CalculateActorRequiredTargetValuePerTurn { get; }
    }
}
