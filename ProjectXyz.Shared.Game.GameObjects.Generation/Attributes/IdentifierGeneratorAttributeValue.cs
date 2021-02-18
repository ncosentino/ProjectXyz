using System;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class IdentifierGeneratorAttributeValue : IGeneratorAttributeValue
    {
        private readonly Lazy<IIdentifier> _lazyValue;

        public IdentifierGeneratorAttributeValue(IIdentifier value)
            : this(() => value)
        {
            Contract.RequiresNotNull(
                value,
                $"{nameof(value)} cannot be null.");
        }

        public IdentifierGeneratorAttributeValue(Func<IIdentifier> callback)
        {
            _lazyValue = new Lazy<IIdentifier>(() =>
            {
                var id = callback();
                Contract.RequiresNotNull(
                    id,
                    $"Identifier cannot be null.");
                return id;
            });
        }

        public IIdentifier Value => _lazyValue.Value;

        public override string ToString() =>
            $"ID: {Value}";
    }
}