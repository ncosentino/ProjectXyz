﻿using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Sql.Properties;

namespace ProjectXyz.Data.Sql
{
    public sealed class SqlDataStore : IDataStore
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IDatabaseUpgrader _upgrader;
        private readonly IEnchantmentRepository _enchantmentRepository;
        #endregion

        #region Constructors
        private SqlDataStore(IDatabase database, IDatabaseUpgrader upgrader)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(upgrader != null);
            
            _database = database;
            _upgrader = upgrader;

            _enchantmentRepository = Enchantments.EnchantmentRepository.Create(
                _database,
                EnchantmentFactory.Create());            

            CreateOrUpgrade();
        }
        #endregion

        #region Properties
        public IEnchantmentRepository EnchantmentRepository
        {
            get { return _enchantmentRepository; }
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

        private void CreateOrUpgrade()
        {
            _database.Execute("PRAGMA foreign_keys=1");
            _database.Execute("PRAGMA journal_mode=WAL");

            int version = GetCurrentSchemaVersion();

            if (version > _upgrader.CurrentSchemaVersion)
            {
                throw new InvalidOperationException(string.Format(
                    "The schema version of the database ({0}) is later than the expected current version ({1}).",
                    version,
                    _upgrader.CurrentSchemaVersion));
            }

            if (version < _upgrader.CurrentSchemaVersion)
            {
                _upgrader.UpgradeDatabase(
                    _database, 
                    version, 
                    _upgrader.CurrentSchemaVersion);
            }
        }

        private int GetCurrentSchemaVersion()
        {
            using (var reader = _database.Query("PRAGMA user_version"))
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
