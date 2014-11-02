using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Items;

namespace ProjectXyz.Core.Items
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

    public class MutableDurability : Durability, IMutableDurability
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
            return new MutableDurability(maximum, current);
        }

        public static IMutableDurability Clone(IDurability durability)
        {
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
