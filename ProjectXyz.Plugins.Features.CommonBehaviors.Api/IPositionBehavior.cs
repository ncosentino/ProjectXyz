namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IPositionBehavior : IObservablePositionBehavior
    {
        void SetPosition(double x, double y);
    }
}