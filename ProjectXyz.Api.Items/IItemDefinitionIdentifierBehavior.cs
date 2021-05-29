
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api
{
    public interface IItemDefinitionIdentifierBehavior : IBehavior
    {
        IIdentifier ItemDefinitionId { get; }
    }
}