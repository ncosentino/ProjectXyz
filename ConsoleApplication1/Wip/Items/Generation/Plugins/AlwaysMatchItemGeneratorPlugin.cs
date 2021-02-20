using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class AlwaysMatchItemGeneratorPlugin : IItemGenerator
    {
        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            yield break;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = new IFilterAttribute[0];
    }
}