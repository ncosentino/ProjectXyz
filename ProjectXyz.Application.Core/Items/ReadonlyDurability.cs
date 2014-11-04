using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class ReadonlyDurability : Durability
    {
        #region Constructors
        private ReadonlyDurability(int maximum, int current)
            : base(maximum, current)
        {
            Contract.Requires<ArgumentException>(maximum >= 0);
            Contract.Requires<ArgumentException>(current >= 0);
            Contract.Requires<ArgumentException>(maximum >= current);
        }
        #endregion

        #region Methods
        public static IDurability Create(int maximum, int current)
        {
            Contract.Requires<ArgumentException>(maximum >= 0);
            Contract.Requires<ArgumentException>(current >= 0);
            Contract.Requires<ArgumentException>(maximum >= current);
            Contract.Ensures(Contract.Result<IDurability>() != null);
            return new ReadonlyDurability(maximum, current);
        }

        public static IDurability Clone(IDurability durability)
        {
            Contract.Requires<ArgumentNullException>(durability != null);
            Contract.Ensures(Contract.Result<IDurability>() != null);
            return new ReadonlyDurability(
                durability.Maximum,
                durability.Current);
        }
        #endregion
    }
}
