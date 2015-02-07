using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Data.Core.Maps
{
    public class MapStore : IMapStore
    {
        #region Fields
        private Guid _id;
        #endregion

        #region Constructors
        private MapStore(Guid id)
        {
            _id = id;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }
        #endregion

        #region Methods
        public static IMapStore Create(Guid id)
        {
            Contract.Ensures(Contract.Result<IMapStore>() != null);
            return new MapStore(id);
        }
        #endregion
    }
}
