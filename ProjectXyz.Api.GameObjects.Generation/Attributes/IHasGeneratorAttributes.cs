using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public interface IHasGeneratorAttributes
    {
        IEnumerable<IGeneratorAttribute> SupportedAttributes { get; }
    }
}