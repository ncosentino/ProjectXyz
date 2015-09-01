using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IDisplayLanguage
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }
        #endregion
    }
}
