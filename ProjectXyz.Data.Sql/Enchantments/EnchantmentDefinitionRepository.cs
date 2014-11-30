using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentDefinitionRepository : IEnchantmentDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentDefinitionFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentDefinitionRepository(IDatabase database, IEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionRepository Create(IDatabase database, IEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionRepository>() != null);

            return new EnchantmentDefinitionRepository(database, factory);
        }

        public IEnchantmentDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Enchantments
                WHERE
                    Id = @id
                LIMIT 1",
                "id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment with Id '" + id + "' was found.");
                    }

                    return EnchantmentFromReader(reader, _factory);
                }
            }
        }
        
        private IEnchantmentDefinition EnchantmentFromReader(IDataReader reader, IEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            var id = reader.GetGuid(reader.GetOrdinal("Id"));
            var statId = reader.GetGuid(reader.GetOrdinal("StatId"));
            var calculationId = reader.GetGuid(reader.GetOrdinal("CalculationId"));
            var triggerId = reader.GetGuid(reader.GetOrdinal("TriggerId"));
            var statusTypeId = reader.GetGuid(reader.GetOrdinal("StatusTypeId"));
            var minimumValue = reader.GetDouble(reader.GetOrdinal("MinimumValue"));
            var maximumValue = reader.GetDouble(reader.GetOrdinal("MaximumValue"));
            var minimumDuration = TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("MinimumDuration")));
            var maximumDuration = TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("MaximumDuration")));

            return factory.CreateEnchantmentDefinition(
                id,
                statId,
                calculationId,
                triggerId,
                statusTypeId,
                minimumValue,
                maximumValue,
                minimumDuration,
                maximumDuration);
        }
        #endregion
    }
}
