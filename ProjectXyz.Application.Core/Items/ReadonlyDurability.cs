using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class ReadonlyDurability : Durability
    {
        #region Constructors
        private ReadonlyDurability(int maximum, int current)
            : base(maximum, current)
        {
        }
        #endregion

        #region Methods
        public static IDurability Create(int maximum, int current)
        {
            return new ReadonlyDurability(maximum, current);
        }

        public static IDurability Clone(IDurability durability)
        {
            return new ReadonlyDurability(
                durability.Maximum,
                durability.Current);
        }
        #endregion
    }
}
