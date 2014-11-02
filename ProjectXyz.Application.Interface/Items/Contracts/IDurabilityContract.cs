using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Contract.Requires(Maximum >= Current);
                Contract.Requires(Maximum >= 0);
                return default(int);
            }
        }

        public int Current
        {
            get
            {
                Contract.Requires(Current <= Maximum);
                Contract.Requires(Current >= 0);
                return default(int);
            }
        }
        #endregion
    }
}
