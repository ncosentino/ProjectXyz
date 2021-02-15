using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    /// <summary>
    /// These extensions are not located with the API interfaces because they 
    /// rely on the concrete type <see cref="GeneratorAttribute"/>.
    /// </summary>
    public static class GeneratorAttributeExtensionMethods
    {
        public static IGeneratorAttribute CopyWithRequired(
            this IGeneratorAttribute generatorAttribute,
            bool required)
        {
            var copy = new GeneratorAttribute(
                generatorAttribute.Id,
                generatorAttribute.Value,
                required);
            return copy;
        }
    }
}