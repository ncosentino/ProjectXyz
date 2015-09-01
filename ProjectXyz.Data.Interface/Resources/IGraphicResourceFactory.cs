using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IGraphicResourceFactory
    {
        #region Methods
        IGraphicResource Create(
            Guid id,
            Guid displayLanguageId,
            string value);
        #endregion
    }
}
