using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
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
