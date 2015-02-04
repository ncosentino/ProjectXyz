using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IDurabilityContract))]
    public interface IDurability
    {
        #region Properties
        int Maximum { get; }
        
        int Current { get; }
        #endregion
    }
}
