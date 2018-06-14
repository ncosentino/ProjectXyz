using System;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class MagicItemGeneratorPlugin : IItemGenerator
    {
        private readonly IBaseItemGenerator _baseItemGenerator;

        public MagicItemGeneratorPlugin(IBaseItemGenerator baseItemGenerator)
        {
            _baseItemGenerator = baseItemGenerator;
        }

        public IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext)
        {
            var items = _baseItemGenerator.GenerateItems(itemGeneratorContext);

            // TODO: mutate

            return items;
        }

        public IEnumerable<IItemGeneratorAttribute> SupportedAttributes { get; } = new IItemGeneratorAttribute[]
        {
            new ItemGeneratorAttribute(new StringIdentifier("affix-type"), new StringItemGeneratorAttributeValue("magic")),
        };
    }
}