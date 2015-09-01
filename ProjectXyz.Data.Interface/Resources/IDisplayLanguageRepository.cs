using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IDisplayLanguageRepository
    {
        #region Methods
        IDisplayLanguage Add(
            Guid id,
            string name);

        IDisplayLanguage GetById(Guid id);

        IEnumerable<IDisplayLanguage> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
