using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class RandomRollItemGeneratorPlugin : IItemGenerator
    {
        public RandomRollItemGeneratorPlugin(
            IIdentifier rollIdentifier,
            double rollChance)
        {
            SupportedAttributes = new IGeneratorAttribute[]
            {
                new GeneratorAttribute(
                    rollIdentifier,
                    new RangeGeneratorAttributeValue(0, rollChance),
                    true),
            };
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            yield break;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; }
    }
}