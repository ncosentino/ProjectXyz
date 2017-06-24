using System.Globalization;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Stats.Core
{
    public sealed class Stat : IStat
    {
        public Stat(IIdentifier statDefinitionId, double value)
        {
            StatDefinitionId = statDefinitionId;
            Value = value;
        }

        public double Value { get; }

        public IIdentifier StatDefinitionId { get; }

        public override string ToString()
        {
            return $"Stat:\r\n\tStat Definition ID: {StatDefinitionId}\r\n\tValue: {Value.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}