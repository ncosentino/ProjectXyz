using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IGraphicResource
    {
        #region Properties
        Guid Id { get; }

        Guid DisplayLanguageId { get; }

        string Value { get; }
        #endregion
    }
}
