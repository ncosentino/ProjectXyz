using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
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
