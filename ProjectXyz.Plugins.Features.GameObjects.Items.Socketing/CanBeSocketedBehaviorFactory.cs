using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanBeSocketedBehaviorFactory : ICanBeSocketedBehaviorFactory
    {
        public ICanBeSocketedBehavior Create(IEnumerable<IIdentifier> totalSocketTypes) =>
            new CanBeSocketedBehavior(totalSocketTypes);
    }
}