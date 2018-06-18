using System.Collections.Generic;

namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public interface IHasItemGeneratorAttributes
    {
        IEnumerable<IItemGeneratorAttribute> SupportedAttributes { get; }
    }
}