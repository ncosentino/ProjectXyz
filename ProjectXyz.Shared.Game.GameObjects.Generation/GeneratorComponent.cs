using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation
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

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; set; }
    }
}