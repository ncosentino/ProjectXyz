namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IPositionBehavior : IObservablePositionBehavior
    {
        void SetPosition(double x, double y);
    }
}