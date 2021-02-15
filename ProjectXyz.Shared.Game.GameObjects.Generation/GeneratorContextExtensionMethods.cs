using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation
{
    /// <summary>
    /// These extensions require access to the class 
    /// <see cref="GeneratorContext"/>. As a result, they cannot be defined in 
    /// the API library.
    /// </summary>
    public static class GeneratorContextExtensionMethods
    {
        public static IGeneratorContext WithAdditionalAttributes(
            this IGeneratorContext generatorContext,
            IEnumerable<IGeneratorAttribute> additionalGeneratorAttributes)
        {
            var newContext = new GeneratorContext(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount,
                generatorContext
                    .Attributes
                    .Concat(additionalGeneratorAttributes));
            return newContext;
        }

        public static IGeneratorContext WithGenerateCountRange(
            this IGeneratorContext generatorContext,
            int minimumGenerateCount,
            int maximumGenerateCount)
        {
            var newContext = new GeneratorContext(
                minimumGenerateCount,
                maximumGenerateCount,
                generatorContext.Attributes);
            return newContext;
        }
    }
}