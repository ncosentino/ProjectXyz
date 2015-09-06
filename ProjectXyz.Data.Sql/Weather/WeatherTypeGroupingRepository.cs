using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Sql.Weather
{
    public sealed class WeatherTypeGroupingRepository : IWeatherGroupingRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IWeatherGroupingFactory _factory;
        #endregion

        #region Constructors
        private WeatherTypeGroupingRepository(
            IDatabase database,
            IWeatherGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IWeatherGroupingRepository Create(
            IDatabase database,
            IWeatherGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IWeatherGroupingRepository>() != null);

            return new WeatherTypeGroupingRepository(
                database,
                factory);
        }

        public IWeatherGrouping Add(
            Guid id,
            Guid weatherId,
            Guid groupingId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "WeatherTypeId", weatherId },
                { "GroupingId", groupingId},
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    WeatherTypeGroupings
                (
                    Id,
                    WeatherTypeId,
                    GroupingId
                )
                VALUES
                (
                    @Id,
                    @WeatherTypeId,
                    @GroupingId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
            
            var weatherTypeGrouping = _factory.Create(
                id,
                weatherId,
                groupingId);
            return weatherTypeGrouping;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    WeatherTypeGroupings
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IWeatherGrouping GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    WeatherTypeGroupings
                WHERE
                    Id = @Id
                LIMIT 1
                ;",
                "@Id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No weather grouping with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IWeatherGrouping> GetByGroupingId(Guid groupingId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    WeatherTypeGroupings
                WHERE
                    GroupingId = @GroupingId
                LIMIT 1
                ;",
                "@GroupingId",
                groupingId))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return GetFromReader(reader, _factory);
                    }
                }
            }
        }

        public IEnumerable<IWeatherGrouping> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StatDefinitions
                ;"))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return GetFromReader(reader, _factory);
                    }
                }
            }
        }

        private IWeatherGrouping GetFromReader(IDataReader reader, IWeatherGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IWeatherGrouping>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("WeatherId")),
                reader.GetGuid(reader.GetOrdinal("GroupingId")));
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_database != null);
            Contract.Invariant(_factory != null);
        }
        #endregion
    }
}
