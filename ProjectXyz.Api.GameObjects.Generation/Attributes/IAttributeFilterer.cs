using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public interface IAttributeFilterer
    {
        IEnumerable<T> Filter<T>(
            IEnumerable<T> source,
            IGeneratorContext generatorContext)
            where T : IHasGeneratorAttributes;
    }
}