using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Resources.Interface.Languages;

namespace ProjectXyz.Data.Resources.Core.Languages
{
    public sealed class DisplayLanguageFactory : IDisplayLanguageFactory
    {
        #region Constructors
        private DisplayLanguageFactory()
        {
        }
        #endregion

        #region Methods
        public static IDisplayLanguageFactory Create()
        {
            Contract.Ensures(Contract.Result<IDisplayLanguageFactory>() != null);
            return new DisplayLanguageFactory();
        }

        public IDisplayLanguage Create(
            Guid id, 
            string name)
        {
            return DisplayLanguage.Create(
                id,
                name);
        }
        #endregion
    }
}
