using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface ITargetNavigator
    {
        bool AreTargetsEqual(IIdentifier target1, IIdentifier target2);

        bool IsSelf(IIdentifier identifier);

        IIdentifier NavigateDown(IIdentifier identifier);

        IIdentifier NavigateUp(IIdentifier identifier);
    }
}
