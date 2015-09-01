using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Sql
{
    public sealed class SqlDatabaseUpgrader : IDatabaseUpgrader
    {
        #region Constants
        private const int CURRENT_SCHEMA_VERSION = 1;
        #endregion

        #region Constructors
        private SqlDatabaseUpgrader()
        {
        }
        #endregion

        #region Properties
        public int CurrentSchemaVersion
        {
            get { return CURRENT_SCHEMA_VERSION; }
        }
        #endregion

        #region Methods
        public static IDatabaseUpgrader Create()
        {
            Contract.Ensures(Contract.Result<IDatabaseUpgrader>() != null);

            return new SqlDatabaseUpgrader();
        }

        public void UpgradeDatabase(IDatabase database, int sourceSchemaVersion, int destinationSchemaVersion)
        {
            if (destinationSchemaVersion == 1)
            {
                database.Execute(GetCreationString(destinationSchemaVersion));
            }
            else
            {
                throw new NotSupportedException(string.Format(
                    "Cannot upgrade from schema version {0} to version {1}.",
                    sourceSchemaVersion, 
                    destinationSchemaVersion));
            }

            database.Execute(string.Format(
                "PRAGMA user_version={0}", 
                destinationSchemaVersion));
        }
        
        private string GetCreationString(int schemaVersion)
        {
            var resourceName = string.Format(
                "create_schema_{0}", 
                schemaVersion);
            return Properties.Resources.ResourceManager.GetString(resourceName);
        }
        #endregion
    }
}
