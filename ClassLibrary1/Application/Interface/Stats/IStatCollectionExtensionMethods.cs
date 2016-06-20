using System;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Stats
{
    public static class IStatCollectionExtensionMethods
    {
        #region Methods
        public static double GetValueOrDefault(
            this IStatCollection dictionary,
            IIdentifier identifier,
            Func<double> defaultValueCallback)
        {
            return dictionary.ContainsKey(identifier)
                ? dictionary[identifier].Value
                : defaultValueCallback.Invoke();
        }

        public static double GetValueOrDefault(
            this IStatCollection dictionary,
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