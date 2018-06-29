using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation
{
    public sealed class GeneratorContextFactory : IGeneratorContextFactory
    {
        public IGeneratorContext CreateGeneratorContext(
            IGeneratorContext source,
            params IGeneratorAttribute[] attributes)
        {
            var newContext = CreateGeneratorContext(
                source,
                (IEnumerable<IGeneratorAttribute>)attributes);
            return newContext;
        }

        public IGeneratorContext CreateGeneratorContext(
            IGeneratorContext source,
            IEnumerable<IGeneratorAttribute> attributes)
        {
            //
            // TODO: do we need to do fancy intelligent merging?
            //
            var mergedAttributes = source
                .Attributes
                .Concat(attributes);

            var newContext = CreateGeneratorContext(
                source.MinimumGenerateCount,
                source.MaximumGenerateCount,
                mergedAttributes);
            return newContext;
        }

        public IGeneratorContext CreateGeneratorContext(
            int minimumCount,
            int maximumCount,
            params IGeneratorAttribute[] attributes)
        {
            var newContext = CreateGeneratorContext(
                minimumCount,
                maximumCount,
                (IEnumerable<IGeneratorAttribute>)attributes);
            return newContext;
        }

        public IGeneratorContext CreateGeneratorContext(
            int minimumCount,
            int maximumCount,
            IEnumerable<IGeneratorAttribute> attributes)
        {
            var itemGeneratorContext = new GeneratorContext(
                minimumCount,
                maximumCount,
                attributes);
            return itemGeneratorContext;
        }
    }
}