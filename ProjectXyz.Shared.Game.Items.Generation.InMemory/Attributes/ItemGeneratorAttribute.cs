using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
{
    public sealed class ItemGeneratorAttribute : IItemGeneratorAttribute
    {
        public ItemGeneratorAttribute(
            IIdentifier id,
            IItemGeneratorAttributeValue value)
        {
            Id = id;
            Value = value;
        }

        public IIdentifier Id { get; }

        public IItemGeneratorAttributeValue Value { get; }
    }
}