using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Application.Core.Maps
{
    public sealed class MapContext : IMapContext
    {
        #region Constructors
        private MapContext()
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static IMapContext Create()
        {
            Contract.Ensures(Contract.Result<IMapContext>() != null);
            return new MapContext();
        }
        #endregion
    }
}
