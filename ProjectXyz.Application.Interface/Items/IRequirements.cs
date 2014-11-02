using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IRequirements
    {
        #region Properties
        int Level { get; }

        string Race { get; }

        string Class { get; }

        IReadonlyStatCollection<IStat> Stats { get; }
        #endregion
    }
}
