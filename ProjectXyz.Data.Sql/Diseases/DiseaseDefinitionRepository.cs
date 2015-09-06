using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Data.Sql.Diseases
{
    public sealed class DiseaseDefinitionRepository : IDiseaseDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IDiseaseDefinitionFactory _diseaseDefinitionFactory;
        #endregion

        #region Constructors
        private DiseaseDefinitionRepository(
            IDatabase database,
            IDiseaseDefinitionFactory diseaseDefinitionFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(diseaseDefinitionFactory != null);

            _database = database;
            _diseaseDefinitionFactory = diseaseDefinitionFactory;
        }
        #endregion

        #region Methods
        public static IDiseaseDefinitionRepository Create(
            IDatabase database,
            IDiseaseDefinitionFactory diseaseDefinitionFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(diseaseDefinitionFactory != null);
            Contract.Ensures(Contract.Result<IDiseaseDefinitionRepository>() != null);

            return new DiseaseDefinitionRepository(
                database,
                diseaseDefinitionFactory);
        }

        public IDiseaseDefinition GetById(Guid id)
        {
            const string QUERY = @"
                SELECT
                    *
                FROM
                    [Diseases]
                WHERE
                    Id = @Id
                ;";

            using (var reader = _database.Query(QUERY, "@Id", id))
            {
                if (!reader.Read())
                {
                    throw new InvalidOperationException("No disease with Id '" + id + "' was found.");
                }

                var name = reader.GetGuid(reader.GetOrdinal("NameStringResourceId"));
                var diseaseStatesId = reader.GetGuid(reader.GetOrdinal("DiseaseStatesId"));

                return _diseaseDefinitionFactory.Create(
                    id,
                    name,
                    diseaseStatesId);
            }
        }
        #endregion
    }
}
