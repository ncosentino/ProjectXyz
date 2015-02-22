using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IDurableContract))]
    public interface IDurable
    {
        #region Properties
        int MaximumDurability { get; }
        
        int CurrentDurability { get; }
        #endregion
    }
}
