using System;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation.Plugins
{
    public sealed class AlwaysMatchItemGeneratorPlugin : IItemGenerator
    {
        public IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext)
        {
            yield break;
        }

        public IEnumerable<IItemGeneratorAttribute> SupportedAttributes { get; } = new IItemGeneratorAttribute[0];
    }
}