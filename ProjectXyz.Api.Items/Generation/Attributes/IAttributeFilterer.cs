using System.Collections.Generic;

namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public interface IAttributeFilterer
    {
        IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IItemGeneratorContext itemGeneratorContext)
            where T : IHasItemGeneratorAttributes;
    }
}