using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicType
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}
