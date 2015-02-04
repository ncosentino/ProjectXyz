using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IRequirementsContract))]
    public interface IRequirements
    {
        #region Properties
        int Level { get; }

        string Race { get; }

        string Class { get; }

        IStatCollection Stats { get; }
        #endregion
    }
}
