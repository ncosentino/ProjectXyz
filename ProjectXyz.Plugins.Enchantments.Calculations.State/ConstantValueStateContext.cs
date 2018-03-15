using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Framework.Extensions.Collections;

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
            double value;
            return _stateToValueMapping.TryGetValue(stateId, out value)
                ? value
                : 0;
        }
    }
}
