using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Data.Sql.Maps
{
    public sealed class MapStoreRepository : IMapStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IMapStoreFactory _factory;
        #endregion

        #region Constructors
        private MapStoreRepository(IDatabase database, IMapStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IMapStoreRepository Create(IDatabase database, IMapStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMapStoreRepository>() != null);

            return new MapStoreRepository(database, factory);
        }

        public IMapStore GetMapStoreById(Guid mapId)
        {
            // TODO: implement reading from the database
            return _factory.CreateMapStore(mapId, false);
        }
        #endregion
    }
}
