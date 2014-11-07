using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableEquipment : IEquipment
    {
        #region Properties
        new IItem this[string slot] { get; set; }
        #endregion
    }
}
