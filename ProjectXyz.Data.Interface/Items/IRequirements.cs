using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Items.Contracts;

namespace ProjectXyz.Data.Interface.Items
{
    [ContractClass(typeof(IRequirementsContract))]
    public interface IRequirements
    {
        #region Properties
        int Level { get; set; }

        string Race { get; set; }

        string Class { get; set; }

        IMutableStatCollection Stats { get; }
        #endregion
    }
}
