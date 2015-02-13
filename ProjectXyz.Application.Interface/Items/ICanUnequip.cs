using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface ICanUnequip
    {
        #region Methods
        IItem Unequip(string slot);

        bool CanUnequip(string slot);
        #endregion
    }
}
