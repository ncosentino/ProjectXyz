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
    [ContractClassFor(typeof(IMutableRequirements))]
    public abstract class IMutableRequirementsContract : IMutableRequirements
    {
        #region Properties
        public abstract int Level { get; }

        public abstract string Race { get; }

        public abstract string Class { get; }

        public abstract IStatCollection<IStat> Stats { get; }

        int IMutableRequirements.Level
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

        string IMutableRequirements.Race
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        string IMutableRequirements.Class
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        IMutableStatCollection<IStat> IMutableRequirements.Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IMutableStatCollection<IStat>>() != null);
                return default(IMutableStatCollection<IStat>);
            }
        }
        #endregion
    }
}
