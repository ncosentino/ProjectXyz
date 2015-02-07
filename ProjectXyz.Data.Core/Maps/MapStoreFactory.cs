using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Data.Core.Maps
{
    public sealed class MapStoreFactory : IMapStoreFactory
    {
        #region Constructors
        private MapStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IMapStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IMapStoreFactory>() != null);

            return new MapStoreFactory();
        }

        public IMapStore CreateMapStore()
        {
            return MapStore.Create();
        }
        #endregion
    }
}
