using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public static class IGeneratorContextFactoryExtensions
    {
        public static IGeneratorContext CreateGeneratorContextForSingle(
            this IGeneratorContextFactory generatorContextFactory,
            params IGeneratorAttribute[] attributes)
        {
            var generatorContext = generatorContextFactory.CreateGeneratorContext(
                1,
                1,
                attributes);
            return generatorContext;
        }

        public static IGeneratorContext CreateGeneratorContextForSingle(
            this IGeneratorContextFactory generatorContextFactory,
            IEnumerable<IGeneratorAttribute> attributes)
        {
            var generatorContext = generatorContextFactory.CreateGeneratorContext(
                1,
                1,
                attributes);
            return generatorContext;
        }

        public static IGeneratorContext CreateGeneratorContextForAnyAmount(
            this IGeneratorContextFactory generatorContextFactory,
            params IGeneratorAttribute[] attributes)
        {
            var generatorContext = generatorContextFactory.CreateGeneratorContext(
                0,
                int.MaxValue,
                attributes);
            return generatorContext;
        }

        public static IGeneratorContext CreateGeneratorContextForAnyAmount(
            this IGeneratorContextFactory generatorContextFactory,
            IEnumerable<IGeneratorAttribute> attributes)
        {
            var generatorContext = generatorContextFactory.CreateGeneratorContext(
                0,
                int.MaxValue,
                attributes);
            return generatorContext;
        }

        public static IGeneratorContext CreateNoneGeneratorContext(this IGeneratorContextFactory generatorContextFactory)
        {
            var generatorContext = generatorContextFactory.CreateGeneratorContext(
                0,
                0,
                Enumerable.Empty<IGeneratorAttribute>());
            return generatorContext;
        }
    }
}