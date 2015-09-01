using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IDisplayLanguageFactory
    {
        #region Methods
        IDisplayLanguage Create(
            Guid id,
            string name);
        #endregion
    }
}
