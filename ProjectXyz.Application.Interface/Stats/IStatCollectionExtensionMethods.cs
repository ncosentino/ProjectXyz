using System;
using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public static class IStatCollectionExtensionMethods
    {
        #region Methods
        public static double GetValueOrDefault(
            this IReadOnlyDictionary<IIdentifier, IStat> dictionary,
            IIdentifier identifier,
            Func<double> defaultValueCallback)
        {
            return dictionary.ContainsKey(identifier)
                ? dictionary[identifier].Value
                : defaultValueCallback.Invoke();
        }

        public static double GetValueOrDefault(
            this IReadOnlyDictionary<IIdentifier, IStat> dictionary,
            IIdentifier identifier,
            double defaultValue)
        {
            return dictionary.ContainsKey(identifier)
                ? dictionary[identifier].Value
                : defaultValue;
        }
        #endregion
    }
}