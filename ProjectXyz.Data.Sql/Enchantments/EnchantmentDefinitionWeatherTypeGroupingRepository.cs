using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentDefinitionWeatherTypeGroupingRepository : IEnchantmentDefinitionWeatherTypeGroupingRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentDefinitionWeatherGroupingFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentDefinitionWeatherTypeGroupingRepository(
            IDatabase database,
            IEnchantmentDefinitionWeatherGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionWeatherTypeGroupingRepository Create(
            IDatabase database,
            IEnchantmentDefinitionWeatherGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionWeatherTypeGroupingRepository>() != null);

            return new EnchantmentDefinitionWeatherTypeGroupingRepository(
                database,
                factory);
        }

        public IEnchantmentDefinitionWeatherGrouping Add(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid weatherTypeDefinitionId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
                { "WeatherTypeDefinitionId", weatherTypeDefinitionId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    EnchantmentDefinitionWeatherGroupings
                (
                    Id,
                    EnchantmentDefinitionId,
                    WeatherTypeDefinitionId
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @WeatherTypeDefinitionId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var enchantmentDefinitionWeatherGrouping = _factory.Create(
                id,
                enchantmentDefinitionId,
                weatherTypeDefinitionId);
            return enchantmentDefinitionWeatherGrouping;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    EnchantmentDefinitionWeatherGroupings
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IEnchantmentDefinitionWeatherGrouping GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    EnchantmentDefinitionWeatherGroupings
                WHERE
                    Id = @id
                LIMIT 1
                ;",
                "@id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment definition weather type grouping with Id '" + id + "' was found.");
                    }

                    var enchantmentTypeWeatherTypeGrouping = EnchantmentFromReader(
                        reader,
                        _factory);
                    return enchantmentTypeWeatherTypeGrouping;
                }
            }
        }

        public IEnchantmentDefinitionWeatherGrouping GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    EnchantmentDefinitionWeatherGroupings
                WHERE
                    EnchantmentDefinitionId = @EnchantmentDefinitionId
                LIMIT 1
                ;",
                "@EnchantmentDefinitionId",
                enchantmentDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment definition weather type grouping with enchantment definition Id '" + enchantmentDefinitionId + "' was found.");
                    }

                    var enchantmentTypeWeatherTypeGrouping = EnchantmentFromReader(
                        reader,
                        _factory);
                    return enchantmentTypeWeatherTypeGrouping;
                }
            }
        }

        private IEnchantmentDefinitionWeatherGrouping EnchantmentFromReader(IDataReader reader, IEnchantmentDefinitionWeatherGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionWeatherGrouping>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("WeatherTypeDefinitionId")));
        }
        #endregion
    }
}
