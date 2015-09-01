using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Core.Resources
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
