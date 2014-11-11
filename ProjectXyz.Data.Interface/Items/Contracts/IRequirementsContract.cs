using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items.Contracts
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

            set
            {
                Contract.Requires<ArgumentNullException>(value >= 0);
            }
        }

        public string Race
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }

        public string Class
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }

        public IMutableStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IMutableStatCollection>() != null);
                return default(IMutableStatCollection);
            }
        }
        #endregion
    }
}
