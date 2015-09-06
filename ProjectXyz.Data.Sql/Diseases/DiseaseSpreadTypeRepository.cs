using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Data.Sql.Diseases
{
    public sealed class DiseaseSpreadTypeRepository : IDiseaseSpreadTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IDiseaseSpreadTypeFactory _diseaseSpreadTypeFactory;
        #endregion

        #region Constructors
        private DiseaseSpreadTypeRepository(
            IDatabase database,
            IDiseaseSpreadTypeFactory diseaseSpreadTypeFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(diseaseSpreadTypeFactory != null);

            _database = database;
            _diseaseSpreadTypeFactory = diseaseSpreadTypeFactory;
        }
        #endregion

        #region Methods
        public static IDiseaseSpreadTypeRepository Create(
            IDatabase database,
            IDiseaseSpreadTypeFactory diseaseSpreadTypeFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(diseaseSpreadTypeFactory != null);
            Contract.Ensures(Contract.Result<IDiseaseSpreadTypeRepository>() != null);

            return new DiseaseSpreadTypeRepository(
                database,
                diseaseSpreadTypeFactory);
        }

        public IDiseaseSpreadType GetById(Guid id)
        {
            const string QUERY = @"
                SELECT
                    *
                FROM
                    [DiseaseSpreadTypes]
                WHERE
                    Id = @Id
                ;";

            using (var reader = _database.Query(QUERY, "@Id", id))
            {
                if (!reader.Read())
                {
                    throw new InvalidOperationException("No disease spread type with Id '" + id + "' was found.");
                }

                var name = reader.GetString(reader.GetOrdinal("Name"));
                return _diseaseSpreadTypeFactory.Create(
                    id,
                    name);
            }
        }
        #endregion
    }
}
