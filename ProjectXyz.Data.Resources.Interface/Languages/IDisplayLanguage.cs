using System;

namespace ProjectXyz.Data.Resources.Interface.Languages
{
    public interface IDisplayLanguage
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }
        #endregion
    }
}
