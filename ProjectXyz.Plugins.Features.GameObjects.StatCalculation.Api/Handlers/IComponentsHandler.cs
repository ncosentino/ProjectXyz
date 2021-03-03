using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers
{
    public interface IComponentsHandler
    {
        IEnumerable<IComponent> HandleComponents(
            IHasBehaviors hasBehaviors,
            IReadOnlyCollection<IComponent> components);
    }
}
