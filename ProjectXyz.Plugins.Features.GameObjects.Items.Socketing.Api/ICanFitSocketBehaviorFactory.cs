using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api
{
    public interface ICanFitSocketBehaviorFactory
    {
        ICanFitSocketBehavior Create(IIdentifier socketType, int socketSize);
    }
}