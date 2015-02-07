using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Core.Actors;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Maps;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Data.Interface.Maps;
using ProjectXyz.Data.Sql.Actors;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Sql.Maps;

namespace ProjectXyz.Data.Sql
{
    public sealed class SqlDataStore : IDataStore
    {
        #region Fields
        private readonly IEnchantmentStoreRepository _enchantmentRepository;
        private readonly IActorStoreRepository _actorStoreRepository;
        private readonly IMapStoreRepository _mapStoreRepository;
        private readonly IDiseaseSpreadTypeRepository _diseaseSpreadTypeRepository;
        private readonly IDiseaseStatesEnchantmentsRepository _diseaseStatesEnchantmentsRepository;
        #endregion

        #region Constructors
        private SqlDataStore(IDatabase database, IDatabaseUpgrader upgrader)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(upgrader != null);

            _enchantmentRepository = EnchantmentStoreRepository.Create(
                database,
                EnchantmentStoreFactory.Create());

            _actorStoreRepository = ActorStoreRepository.Create(
                database,
                ActorStoreFactory.Create());

            _mapStoreRepository = MapStoreRepository.Create(
                database,
                MapStoreFactory.Create());

            CreateOrUpgrade(database, upgrader);
        }
        #endregion

        #region Properties
        public IEnchantmentStoreRepository Enchantments
        {
            get { return _enchantmentRepository; }
        }

        public IActorStoreRepository Actors
        {
            get { return _actorStoreRepository; }
        }

        public IMapStoreRepository Maps
        {
            get { return _mapStoreRepository; }
        }

        public IDiseaseSpreadTypeRepository DiseaseSpreadType
        {
            get { return _diseaseSpreadTypeRepository; }
        }

        public IDiseaseStatesEnchantmentsRepository DiseaseStatesEnchantments
        {
            get { return _diseaseStatesEnchantmentsRepository; }
        }
        #endregion

        #region Methods
        public static IDataStore Create(IDatabase database, IDatabaseUpgrader upgrader)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(upgrader != null);
            Contract.Ensures(Contract.Result<IDataStore>() != null);

            return new SqlDataStore(database, upgrader);
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
