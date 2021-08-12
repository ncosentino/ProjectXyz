using System;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public sealed class StatChangedEventArgs : EventArgs
    {
        public StatChangedEventArgs(
            StatChanged status,
            IIdentifier statDefinitionId,
            double? value,
            double? oldValue)
        {
            Status = status;
            StatDefinitionId = statDefinitionId;
            Value = value;
            OldValue = oldValue;
        }

        public StatChanged Status { get; }

        public IIdentifier StatDefinitionId { get; }

        public double? Value { get; }

        public double? OldValue { get; }
    }
}