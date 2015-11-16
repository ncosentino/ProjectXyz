using System;

namespace ProjectXyz.Data.Resources.Interface.Languages
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
