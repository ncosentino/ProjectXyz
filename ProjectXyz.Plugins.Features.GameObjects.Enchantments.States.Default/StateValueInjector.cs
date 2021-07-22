using System;
using System.Collections.Generic;
using System.Globalization;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
{
    public sealed class StateValueInjector : IStateValueInjector
    {
        private readonly Lazy<IReadOnlyStateManager> _lazyStateManager;
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>> _stateIdToTermMapping;

        public StateValueInjector(
            Lazy<IReadOnlyStateManager> lazyStateManager,
            IReadOnlyDictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>> stateIdToTermMapping)
        {
            _lazyStateManager = lazyStateManager;
            _stateIdToTermMapping = stateIdToTermMapping;
        }

        public string Inject(string expression)
        {
            foreach (var stateTypeIdToStateTermMapping in _stateIdToTermMapping)
            {
                var stateTypeId = stateTypeIdToStateTermMapping.Key;
                foreach (var stateIdToTermMapping in stateTypeIdToStateTermMapping.Value)
                {
                    var stateId = stateIdToTermMapping.Key;
                    var term = stateIdToTermMapping.Value;

                    var states = _lazyStateManager.Value.GetStates(stateTypeId);
                    if (!states.TryGetValue(stateId, out var value))
                    {
                        continue;
                    }

                    if (value is double doubleValue)
                    {
                        expression = expression.Replace(
                            term, 
                            Convert.ToString(doubleValue, CultureInfo.InvariantCulture));
                    }
                    else if (value is string stringValue)
                    {
                        expression = expression.Replace(term, stringValue);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            $"There is no support to convert '{value}' into a " +
                            $"form that can be used by expressions. You may " +
                            $"have stored the state incorrectly (perhaps we " +
                            $"can prevent this) or it may be that we need to " +
                            $"extend support for this type of data in our " +
                            $"expressions.");
                    }                    
                }
            }

            return expression;
        }
    }
}