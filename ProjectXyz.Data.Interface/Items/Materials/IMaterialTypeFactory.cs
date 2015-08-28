using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items.Materials
{
    public interface IMaterialTypeFactory
    {
        #region Methods
        IMaterialType Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
