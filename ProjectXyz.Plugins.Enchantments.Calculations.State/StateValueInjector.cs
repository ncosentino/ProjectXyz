using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class StateValueInjector : IStateValueInjector
    {
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>> _stateIdToTermMapping;

        public StateValueInjector(IReadOnlyDictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>> stateIdToTermMapping)
        {
            _stateIdToTermMapping = stateIdToTermMapping;
        }

        public string Inject(
            IStateContextProvider stateContextProvider,
            string expression)
        {
            foreach (var stateTypeIdToStateTermMapping in _stateIdToTermMapping)
            {
                var stateTypeId = stateTypeIdToStateTermMapping.Key;
                foreach (var stateIdToTermMapping in stateTypeIdToStateTermMapping.Value)
                {
                    var stateId = stateIdToTermMapping.Key;
                    var term = stateIdToTermMapping.Value;

                    IStateContext stateContext;
                    if (!stateContextProvider.TryGetValue(stateTypeId, out stateContext))
                    {
                        continue;
                    }

                    var value = stateContext.GetStateValue(stateId);
                    expression = expression.Replace(term, $"({value})");
                }
            }

            return expression;
        }
    }
}