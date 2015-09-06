using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Core.Actors;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Core.Maps;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Maps;
using ProjectXyz.Data.Interface.Resources;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Data.Sql.Actors;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Sql.Items;
using ProjectXyz.Data.Sql.Maps;
using ProjectXyz.Data.Sql.Resources;
using ProjectXyz.Data.Sql.Stats;
using ProjectXyz.Data.Sql.Weather;

namespace ProjectXyz.Data.Sql
{
    public sealed class SqlDataManager : IDataManager
    {
        #region Fields
        private readonly IActorStoreRepository _actorStoreRepository;
        private readonly IMapStoreRepository _mapStoreRepository;
        private readonly IItemDataManager _itemDataManager;
        private readonly IEnchantmentsDataManager _enchantmentsDataManager;
        private readonly IStatsDataManager _statsDataManager;
        private readonly IResourcesDataManager _resourcesDataManager;
        private readonly IWeatherDataManager _weatherDataManager;
        #endregion

        #region Constructors
        private SqlDataManager(IDatabase database, IDatabaseUpgrader upgrader)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(upgrader != null);

            _resourcesDataManager = SqlResourcesDataManager.Create(database);

            _statsDataManager = SqlStatsDataManager.Create(database);
            
            _actorStoreRepository = ActorStoreRepository.Create(
                database,
                ActorStoreFactory.Create());

            _mapStoreRepository = MapStoreRepository.Create(
                database,
                MapStoreFactory.Create());

            _itemDataManager = SqlItemDataManager.Create(database);

            _enchantmentsDataManager = SqlEnchantmentsDataManager.Create(database);

            _weatherDataManager = SqlWeatherDataManager.Create(database);

            CreateOrUpgrade(database, upgrader);
        }
        #endregion

        #region Properties
        public IActorStoreRepository Actors
        {
            get { return _actorStoreRepository; }
        }

        public IMapStoreRepository Maps
        {
            get { return _mapStoreRepository; }
        }

        public IEnchantmentsDataManager Enchantments
        {
            get { return _enchantmentsDataManager; }
        }

        public IStatsDataManager Stats
        {
            get { return _statsDataManager; }
        }

        public IDiseaseDataManager Diseases
        {
            get { throw new NotImplementedException(); }
        }

        public IItemDataManager Items
        {
            get { return _itemDataManager; }
        }

        public IWeatherDataManager Weather
        {
            get { return _weatherDataManager; }
        }

        public  IResourcesDataManager Resources
        {
            get { return _resourcesDataManager; }
        }
        #endregion

        #region Methods
        public static IDataManager Create(IDatabase database, IDatabaseUpgrader upgrader)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(upgrader != null);
            Contract.Ensures(Contract.Result<IDataManager>() != null);

            return new SqlDataManager(database, upgrader);
        }

        private void CreateOrUpgrade(IDatabase database, IDatabaseUpgrader upgrader)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(upgrader != null);

            database.Execute("PRAGMA foreign_keys=1");
            database.Execute("PRAGMA journal_mode=WAL");

            int version = GetCurrentSchemaVersion(database);

            if (version > upgrader.CurrentSchemaVersion)
            {
                throw new InvalidOperationException(string.Format(
                    "The schema version of the database ({0}) is later than the expected current version ({1}).",
                    version,
                    upgrader.CurrentSchemaVersion));
            }

            if (version < upgrader.CurrentSchemaVersion)
            {
                upgrader.UpgradeDatabase(
                    database, 
                    version, 
                    upgrader.CurrentSchemaVersion);
            }
        }

        private int GetCurrentSchemaVersion(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);

            using (var reader = database.Query("PRAGMA user_version"))
            {
                if (!reader.Read())
                {
                    throw new InvalidOperationException("Could not read the current schema version.");
                }

                return reader.GetInt32(reader.GetOrdinal("user_version"));
            }
        }
        #endregion
    }
}
