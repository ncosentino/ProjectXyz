using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Application.Core.Maps
{
    public sealed class Map : IMap
    {
        #region Fields
        private readonly IMapContext _context;
        private readonly IMapStore _store;
        #endregion

        #region Constructors
        private Map(IMapContext context, IMapStore mapStore)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(mapStore != null);

            _context = context;
            _store = mapStore;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _store.Id; }
        }

        public string ResourceName
        {
            get { return "Assets/Resources/Maps/Swamp.tmx"; }
        }
        #endregion

        #region Methods
        public static IMap Create(IMapContext context, IMapStore mapStore)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(mapStore != null);

            Contract.Ensures(Contract.Result<IMap>() != null);
            return new Map(context, mapStore);
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_context != null);
        }
        #endregion
    }
}
