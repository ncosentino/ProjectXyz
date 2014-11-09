﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Core.Stats;

namespace ProjectXyz.Data.Core.Actors
{
    public class Actor : IActor
    {
        #region Fields
        private IMutableStatCollection _stats;
        #endregion

        #region Constructors
        private Actor()
        {
            _stats = StatCollection.Create();
            _stats.Set(Stat.Create(ActorStats.MaximumLife, 0));
            _stats.Set(Stat.Create(ActorStats.CurrentLife, 0));
        }
        #endregion

        #region Properties
        public IMutableStatCollection Stats
        {
            get { return _stats; }
        }
        #endregion

        #region Methods
        public static IActor Create()
        {
            Contract.Ensures(Contract.Result<IActor>() != null);
            return new Actor();
        }
        #endregion
    }
}
