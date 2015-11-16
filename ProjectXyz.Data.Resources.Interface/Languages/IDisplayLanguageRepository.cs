using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Resources.Interface.Languages
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
