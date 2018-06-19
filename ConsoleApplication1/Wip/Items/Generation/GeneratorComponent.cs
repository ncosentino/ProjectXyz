using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Items.Generation;

namespace ConsoleApplication1.Wip.Items.Generation
{
    public sealed class GeneratorComponent : IGeneratorComponent
    {
        public GeneratorComponent()
            : this(Enumerable.Empty<IGeneratorAttribute>())
        {
        }

        public GeneratorComponent(IEnumerable<IGeneratorAttribute> supportedAttributes)
        {
            SupportedAttributes = supportedAttributes.ToArray();
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; }
    }
}