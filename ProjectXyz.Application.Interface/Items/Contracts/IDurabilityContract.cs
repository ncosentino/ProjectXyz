using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IDurability))]
    public abstract class IDurabilityContract : IDurability
    {
        #region Properties
        public int Maximum
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= Current);
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public int Current
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() <= Maximum);
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }
        #endregion
    }
}
