using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IRequirements))]
    public abstract class IRequirementsContract : IRequirements
    {
        #region Properties
        public int Level
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public string Race
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
        }

        public string Class
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
        }

        public IStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatCollection>() != null);
                return default(IStatCollection);
            }
        }
        #endregion
    }
}
