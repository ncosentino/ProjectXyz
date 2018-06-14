using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class RandomRollItemGeneratorPlugin : IItemGenerator
    {
        public RandomRollItemGeneratorPlugin(
            IIdentifier rollIdentifier,
            double rollChance)
        {
            SupportedAttributes = new IItemGeneratorAttribute[]
            {
                new ItemGeneratorAttribute(
                    rollIdentifier,
                    new RangeItemGeneratorAttributeValue(0, rollChance)), 
            };
        }

        public IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext)
        {
            yield break;
        }

        public IEnumerable<IItemGeneratorAttribute> SupportedAttributes { get; }
    }
}