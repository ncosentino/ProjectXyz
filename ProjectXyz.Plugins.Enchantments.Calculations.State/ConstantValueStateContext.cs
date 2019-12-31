using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.States;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class ConstantValueStateContext : IStateContext
    {
        private readonly IReadOnlyDictionary<IIdentifier, double> _stateToValueMapping;

        public ConstantValueStateContext(IEnumerable<KeyValuePair<IIdentifier, double>> stateToValueMapping)
            : this(stateToValueMapping.ToDictionary())
        {
        }

        public ConstantValueStateContext(IReadOnlyDictionary<IIdentifier, double> stateToValueMapping)
        {
            _stateToValueMapping = stateToValueMapping;
        }

        public double GetStateValue(IIdentifier stateId)
        {
            return _stateToValueMapping.TryGetValue(stateId, out var value)
                ? value
                : 0;
        }
    }
}
