using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class RandomRollItemGeneratorPlugin : IItemGenerator
    {
        public RandomRollItemGeneratorPlugin(
            IIdentifier rollIdentifier,
            double rollChance)
        {
            SupportedAttributes = new IFilterAttribute[]
            {
                new FilterAttribute(
                    rollIdentifier,
                    new RangeFilterAttributeValue(0, rollChance),
                    true),
            };
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            yield break;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}