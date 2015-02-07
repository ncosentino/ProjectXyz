using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Application.Core.Maps
{
    public sealed class MapManager : IMapManager
    {
        #region Fields
        private readonly IMapBuilder _mapBuilder;
        private readonly IMapStoreRepository _mapStoreRepository;
        #endregion

        #region Constructors
        private MapManager(IMapBuilder mapBuilder, IMapStoreRepository mapStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(mapBuilder != null, "mapBuilder");
            Contract.Requires<ArgumentNullException>(mapStoreRepository != null, "mapStoreRepository");

            _mapBuilder = mapBuilder;
            _mapStoreRepository = mapStoreRepository;
        }
        #endregion

        #region Methods
        public static IMapManager Create(IMapBuilder mapBuilder, IMapStoreRepository mapStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(mapBuilder != null, "mapBuilder");
            Contract.Requires<ArgumentNullException>(mapStoreRepository != null, "mapStoreRepository");
            Contract.Ensures(Contract.Result<IMapManager>() != null);

            return new MapManager(
                mapBuilder,
                mapStoreRepository);
        }

        public IMap GetMapById(Guid mapId, IMapContext mapContext)
        {
            var mapStore = _mapStoreRepository.GetMapStoreById(mapId);

            return Map.Create(
                _mapBuilder,
                mapContext,
                mapStore);
        }
        #endregion
    }
}
