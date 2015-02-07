﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Data.Core.Maps
{
    public class MapStore : IMapStore
    {
        #region Constructors
        private MapStore()
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static IMapStore Create()
        {
            Contract.Ensures(Contract.Result<IMapStore>() != null);
            return new MapStore();
        }
        #endregion
    }
}
