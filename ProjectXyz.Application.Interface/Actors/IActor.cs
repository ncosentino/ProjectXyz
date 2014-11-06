using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.Actors
{
    public interface IActor
    {
        #region Properties
        double MaximumLife { get; }

        double CurrentLife { get; }
        #endregion

        #region Methods
        bool Equip(IItem item);
        #endregion
    }
}
