using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface ICanEquip
    {
        #region Methods
        bool CanEquip(IItem item, string slot);

        void Equip(IItem item, string slot);
        #endregion
    }
}
