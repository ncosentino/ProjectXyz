using System;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class StringGeneratorAttributeValue : IGeneratorAttributeValue
    {
        private readonly Lazy<string> _lazyValue;

        public StringGeneratorAttributeValue(string value)
            : this (() => value)
        {
        }

        public StringGeneratorAttributeValue(Func<string> callback)
        {
            _lazyValue = new Lazy<string>(callback);
        }

        public string Value => _lazyValue.Value;

        public override string ToString() => Value;
    }
}