using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public abstract class Durability : IDurability
    {
        #region Constructors
        protected Durability()
        {
        }

        protected Durability(int maximum, int current)
            : this()
        {
            this.Maximum = maximum;
            this.Current = current;
        }
        #endregion

        #region Properties
        public int Maximum
        {
            get;
            protected set;
        }

        public int Current
        {
            get;
            protected set;
        }
        #endregion
    }
}
