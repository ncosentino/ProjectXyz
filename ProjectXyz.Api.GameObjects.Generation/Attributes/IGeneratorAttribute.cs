using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public interface IGeneratorAttribute
    {
        IIdentifier Id { get; }

        IGeneratorAttributeValue Value { get; }
    }
}