using System;

using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class DoubleGeneratorAttributeValue : IGeneratorAttributeValue
    {
        private readonly Lazy<double> _lazyValue;

        public DoubleGeneratorAttributeValue(double value)
            : this(() => value)
        {
        }

        public DoubleGeneratorAttributeValue(Func<double> callback)
        {
            _lazyValue = new Lazy<double>(callback);
        }

        public double Value => _lazyValue.Value;

        public override string ToString() =>
            $"{Value}d";
    }
}