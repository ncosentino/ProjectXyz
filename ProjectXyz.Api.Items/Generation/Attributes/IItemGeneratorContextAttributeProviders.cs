using System.Collections.Generic;

namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public interface IItemGeneratorContextAttributeProvider
    {
        IEnumerable<IItemGeneratorAttribute> GetAttributes();
    }
}