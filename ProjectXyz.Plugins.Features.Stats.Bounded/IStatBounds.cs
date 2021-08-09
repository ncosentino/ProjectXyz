namespace ProjectXyz.Plugins.Features.Stats.Bounded
{
    public interface IStatBounds
    {
        string MinimumExpression { get; }

        string MaximumExpression { get; }
    }
}