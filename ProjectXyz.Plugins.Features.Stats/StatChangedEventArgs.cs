using System;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public sealed class StatChangedEventArgs : EventArgs
    {
        public StatChangedEventArgs(
            StatChanged status,
            IIdentifier statDefinitionId,
            double? value)
        {
            Status = status;
            StatDefinitionId = statDefinitionId;
            Value = value;
        }

        public StatChanged Status { get; }

        public IIdentifier StatDefinitionId { get; }

        public double? Value { get; }
    }
}