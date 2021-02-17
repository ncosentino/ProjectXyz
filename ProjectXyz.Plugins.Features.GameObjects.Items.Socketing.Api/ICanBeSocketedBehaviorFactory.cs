using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api
{
    public interface ICanBeSocketedBehaviorFactory
    {
        ICanBeSocketedBehavior Create(IEnumerable<IIdentifier> totalSocketTypes);
    }
}