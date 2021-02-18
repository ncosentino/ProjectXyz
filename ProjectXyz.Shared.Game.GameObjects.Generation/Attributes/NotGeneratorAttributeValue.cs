using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class NotGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public NotGeneratorAttributeValue(IGeneratorAttributeValue wrapped)
        {
            Wrapped = wrapped;
        }

        public IGeneratorAttributeValue Wrapped { get; }

        public override string ToString() =>
            $"Not ({Wrapped})";
    }
}