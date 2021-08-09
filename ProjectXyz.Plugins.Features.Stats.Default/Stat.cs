﻿using System.Globalization;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default
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