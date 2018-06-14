using System.Collections.Generic;

namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemGeneratorFilterer
    {
        IEnumerable<IItemGenerator> Filter(
            IEnumerable<IItemGenerator> itemGenerators,
            IItemGeneratorContext itemGeneratorContext);
    }
}