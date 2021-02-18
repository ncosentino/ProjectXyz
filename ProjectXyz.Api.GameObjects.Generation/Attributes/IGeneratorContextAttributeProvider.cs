using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public interface IGeneratorContextAttributeProvider
    {
        IEnumerable<IGeneratorAttribute> GetAttributes();
    }
}