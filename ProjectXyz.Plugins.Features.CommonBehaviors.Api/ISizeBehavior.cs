namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ISizeBehavior : IObservableSizeBehavior
    {
        void SetSize(double width, double height);
    }
}