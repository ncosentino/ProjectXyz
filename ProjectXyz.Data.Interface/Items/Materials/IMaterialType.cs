using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Materials
{
    public interface IMaterialType
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}
