namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatBounds
    {
        string MinimumExpression { get; }

        string MaximumExpression { get; }
    }
}