using System.Globalization;
using ClassLibrary1.Application.Interface.Stats;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Shared.Stats
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