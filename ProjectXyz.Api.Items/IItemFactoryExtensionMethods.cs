using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api
{
    public static class IItemFactoryExtensionMethods
    {
        public static IGameObject Create(
            this IItemFactory @this,
            params IBehavior[] behaviors) => @this.Create((IEnumerable<IBehavior>)behaviors);
    }
}