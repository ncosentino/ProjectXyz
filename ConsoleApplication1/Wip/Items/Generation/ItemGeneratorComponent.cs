using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ConsoleApplication1.Wip.Items.Generation
{
    public sealed class ItemGeneratorComponent : IItemGeneratorComponent
    {
        public ItemGeneratorComponent()
            : this(Enumerable.Empty<IItemGeneratorAttribute>())
        {
        }

        public ItemGeneratorComponent(IEnumerable<IItemGeneratorAttribute> supportedAttributes)
        {
            SupportedAttributes = supportedAttributes.ToArray();
        }

        public IEnumerable<IItemGeneratorAttribute> SupportedAttributes { get; }
    }
}