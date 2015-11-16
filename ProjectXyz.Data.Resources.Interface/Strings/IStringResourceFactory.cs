using System;

namespace ProjectXyz.Data.Resources.Interface.Strings
{
    public interface IStringResourceFactory
    {
        #region Methods
        IStringResource Create(
            Guid id,
            Guid displayLanguageId,
            string value);
        #endregion
    }
}
