namespace ProjectXyz.Plugins.Api.Stats.Bounded
{
    public interface IStatBounds
    {
        string MinimumExpression { get; }

        string MaximumExpression { get; }
    }
}