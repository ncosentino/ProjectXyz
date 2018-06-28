using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class GeneratorAttribute : IGeneratorAttribute
    {
        public GeneratorAttribute(
            IIdentifier id,
            IGeneratorAttributeValue value,
            bool required)
        {
            Id = id;
            Value = value;
            Required = required;
        }

        public IIdentifier Id { get; }

        public IGeneratorAttributeValue Value { get; }

        public bool Required { get; }
    }
}