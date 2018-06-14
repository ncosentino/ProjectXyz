using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public interface IItemGeneratorAttribute
    {
        IIdentifier Id { get; }

        IItemGeneratorAttributeValue Value { get; }
    }
}