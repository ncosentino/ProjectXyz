namespace ProjectXyz.Api.Stats.Bounded
{
    public interface IStatBounds
    {
        string MinimumExpression { get; }

        string MaximumExpression { get; }
    }
}