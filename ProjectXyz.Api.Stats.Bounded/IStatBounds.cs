namespace ProjectXyz.Plugins.Features.BoundedStats.Api
{
    public interface IStatBounds
    {
        string MinimumExpression { get; }

        string MaximumExpression { get; }
    }
}