using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Stats;

namespace ProjectXyz.Interface.Items
{
    public interface IRequirements
    {
        #region Properties
        int Level { get; }

        string Race { get; }

        string Class { get; }

        IStatCollection<IStat> Stats { get; }
        #endregion
    }
}
