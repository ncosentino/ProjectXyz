﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentRepository : IEnchantmentRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentRepository(IDatabase database, IEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentRepository Create(IDatabase database, IEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentRepository>() != null);

            return new EnchantmentRepository(database, factory);
        }

        public IEnchantment Generate(Guid id, Random randomizer)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    StatId, 
                    CalculationId, 
                    TriggerId, 
                    StatusTypeId, 
                    MinimumValue, 
                    MaximumValue,
                    MinimumDuration, 
                    MaximumDuration
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
                        throw new InvalidOperationException("Could not spawn enchantment with Id = '" + id + "'.");
                    }

                    return EnchantmentFromReader(
                        reader, 
                        _factory,
                        randomizer);
                }
            }
        }
        
        private IEnchantment EnchantmentFromReader(IDataReader reader, IEnchantmentFactory factory, Random randomizer)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnchantment>() != null);

            var enchantment = factory.CreateEnchantment();
            enchantment.StatId = reader.GetGuid(reader.GetOrdinal("StatId"));
            enchantment.CalculationId = reader.GetGuid(reader.GetOrdinal("CalculationId"));
            enchantment.TriggerId = reader.GetGuid(reader.GetOrdinal("TriggerId"));
            enchantment.StatusTypeId = reader.GetGuid(reader.GetOrdinal("StatusTypeId"));

            var minimumDurationMs = reader.GetDouble(reader.GetOrdinal("MinimumDuration"));
            var maximumDurationMs = reader.GetDouble(reader.GetOrdinal("MaximumDuration"));
            enchantment.RemainingDuration = TimeSpan.FromMilliseconds(minimumDurationMs + randomizer.NextDouble() * (maximumDurationMs - minimumDurationMs));

            var minimumValue = reader.GetDouble(reader.GetOrdinal("MinimumValue"));
            var maximumValue = reader.GetDouble(reader.GetOrdinal("MaximumValue"));
            enchantment.Value = minimumValue + randomizer.NextDouble() * (maximumValue - minimumValue);

            return enchantment;
        }
        #endregion
    }
}
