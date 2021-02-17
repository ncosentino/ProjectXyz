using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanFitSocketBehaviorFactory : ICanFitSocketBehaviorFactory
    {
        public ICanFitSocketBehavior Create(
            IIdentifier socketType,
            int socketSize) => new CanFitSocketBehavior(
                socketType,
                socketSize);
    }
}