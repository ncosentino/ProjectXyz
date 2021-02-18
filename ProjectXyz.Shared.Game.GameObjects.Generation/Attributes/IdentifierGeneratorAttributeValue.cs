using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class IdentifierGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public IdentifierGeneratorAttributeValue(IIdentifier value)
        {
            Contract.RequiresNotNull(
                value,
                $"{nameof(value)} cannot be null.");

            Value = value;
        }

        public IIdentifier Value { get; }

        public override string ToString() =>
            $"ID: {Value}";
    }
}