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
        private readonly IMapStoreRepository _mapStoreRepository;
        #endregion

        #region Constructors
        private MapManager(IMapStoreRepository mapStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(mapStoreRepository != null, "mapStoreRepository");

            _mapStoreRepository = mapStoreRepository;
        }
        #endregion

        #region Methods
        public static IMapManager Create(IMapStoreRepository mapStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(mapStoreRepository != null, "mapStoreRepository");
            Contract.Ensures(Contract.Result<IMapManager>() != null);

            return new MapManager(mapStoreRepository);
        }

        public IMap GetMapById(Guid mapId, IMapContext mapContext)
        {
            var mapStore = _mapStoreRepository.GetMapStoreById(mapId);

            return Map.Create(
                mapContext,
                mapStore);
        }
        #endregion
    }
}
