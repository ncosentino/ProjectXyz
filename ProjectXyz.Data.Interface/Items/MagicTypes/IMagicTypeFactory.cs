using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicTypeFactory
    {
        #region Methods
        IMagicType Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
