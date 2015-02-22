using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IDurable))]
    public abstract class IDurableContract : IDurable
    {
        #region Properties
        public int MaximumDurability
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= CurrentDurability);
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public int CurrentDurability
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() <= MaximumDurability);
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }
        #endregion
    }
}
