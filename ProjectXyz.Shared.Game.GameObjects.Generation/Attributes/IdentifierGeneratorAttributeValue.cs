using System.Diagnostics;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    [DebuggerDisplay("{Value}")]
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
    }
}