using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorContextFactory
    {
        IGeneratorContext CreateGeneratorContext(
            int minimumCount,
            int maximumCount,
            params IGeneratorAttribute[] attributes);

        IGeneratorContext CreateGeneratorContext(
            int minimumCount,
            int maximumCount,
            IEnumerable<IGeneratorAttribute> attributes);

        IGeneratorContext CreateGeneratorContext(
            IGeneratorContext source,
            params IGeneratorAttribute[] attributes);

        IGeneratorContext CreateGeneratorContext(
            IGeneratorContext source,
            IEnumerable<IGeneratorAttribute> attributes);
    }
}