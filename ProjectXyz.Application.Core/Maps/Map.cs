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
        #endregion

        #region Constructors
        private Map(IMapBuilder builder, IMapContext context, IMapStore mapStore)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(mapStore != null);

            _context = context;
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static IMap Create(IMapBuilder builder, IMapContext context, IMapStore mapStore)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(mapStore != null);

            Contract.Ensures(Contract.Result<IMap>() != null);
            return new Map(builder, context, mapStore);
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_context != null);
        }
        #endregion
    }
}
