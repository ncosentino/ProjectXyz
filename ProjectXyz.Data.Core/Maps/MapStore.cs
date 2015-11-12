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
        #region Constructors
        private MapStore(
            Guid id,
            bool isInterior)
        {
            Id = id;
            IsInterior = isInterior;
        }
        #endregion

        #region Properties
        public Guid Id { get; }
        
        public bool IsInterior { get; }
        #endregion

        #region Methods
        public static IMapStore Create(
            Guid id,
            bool isInterior)
        {
            Contract.Ensures(Contract.Result<IMapStore>() != null);
            return new MapStore(id, isInterior);
        }
        #endregion
    }
}
