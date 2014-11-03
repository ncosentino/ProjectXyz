using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class MutableDurability : Durability, IMutableDurability
    {
        #region Constructors
        private MutableDurability()
            : this(0, 0)
        {
        }

        private MutableDurability(int maximum, int current)
            : base(maximum, current)
        {
        }
        #endregion

        #region Methods
        public static IMutableDurability Create()
        {
            return new MutableDurability();
        }

        public static IMutableDurability Create(int maximum, int current)
        {
            Contract.Requires<ArgumentNullException>(maximum >= 0);
            Contract.Requires<ArgumentNullException>(current >= 0);
            Contract.Requires<ArgumentNullException>(maximum >= current);
            return new MutableDurability(maximum, current);
        }

        public static IMutableDurability Clone(IDurability durability)
        {
            Contract.Requires<ArgumentNullException>(durability != null);
            return new MutableDurability(
                durability.Maximum,
                durability.Current);
        }

        public void SetMaximum(int value)
        {
            this.Maximum = value;
        }

        public void SetCurrent(int value)
        {
            this.Current = value;
        }
        #endregion
    }
}
