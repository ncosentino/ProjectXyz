using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentTrigger))]
    public abstract class IEnchantmentTriggerContract : IEnchantmentTrigger
    {
        #region Properties
        public string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }
        }

        public Guid Id
        {
            get { return default(Guid); }

            set { }
        }
        #endregion
    }
}
