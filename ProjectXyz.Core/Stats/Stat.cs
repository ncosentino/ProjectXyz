using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Stats;

namespace ProjectXyz.Core.Stats
{
    public abstract class Stat : IStat
    {
        #region Constructors
        protected Stat(string id, double value)
            : this(id)
        {
            this.Value = value;
        }

        protected Stat(string id)
        {
            this.Id = id;
        }
        #endregion


        #region Properties
        public string Id
        {
            get;
            private set;
        }

        public double Value
        {
            get;
            protected set;
        }
        #endregion
    }
}
