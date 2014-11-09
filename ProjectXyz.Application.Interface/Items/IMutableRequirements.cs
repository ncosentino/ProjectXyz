using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IMutableRequirementsContract))]
    public interface IMutableRequirements : IRequirements
    {
        #region Properties
        new int Level { get; set; }

        new string Race { get; set; }

        new string Class { get; set; }

        new IMutableStatCollection Stats { get; }
        #endregion
    }
}
