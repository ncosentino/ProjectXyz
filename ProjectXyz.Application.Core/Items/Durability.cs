using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Durability : IMutableDurability
    {
        #region Constructors
        private Durability(int maximum, int current)
        {
            Contract.Requires<ArgumentException>(maximum >= 0);
            Contract.Requires<ArgumentException>(current >= 0);
            Contract.Requires<ArgumentException>(maximum >= current);
            this.Maximum = maximum;
            this.Current = current;
        }
        #endregion

        #region Properties
        public int Maximum
        {
            get;
            set;
        }

        public int Current
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IMutableDurability Create(int maximum, int current)
        {
            Contract.Requires<ArgumentException>(maximum >= 0);
            Contract.Requires<ArgumentException>(current >= 0);
            Contract.Requires<ArgumentException>(maximum >= current);
            Contract.Ensures(Contract.Result<IDurability>() != null);
            return new Durability(maximum, current);
        }
        #endregion
    }
}
