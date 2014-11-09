using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class Requirements : IRequirements
    {
        #region Fields
        private readonly IMutableStatCollection _stats;
        #endregion

        #region Constructors
        private Requirements()
        {
            _stats = StatCollection.Create();
        }
        #endregion
        
        #region Properties
        public int Level
        {
            get;
            set;
        }

        public string Race
        {
            get;
            set;
        }

        public string Class
        {
            get;
            set;
        }

        public IMutableStatCollection Stats
        {
            get { return _stats; }
        }
        #endregion

        #region Methods
        public static IRequirements Create()
        {
            Contract.Ensures(Contract.Result<IRequirements>() != null);
            return new Requirements();
        }
        #endregion       
    }
}
