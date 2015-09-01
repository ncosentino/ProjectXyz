using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Sql.Stats
{
    public sealed class SqlStatsDataStore : IStatsDataStore
    {
        #region Fields
        private readonly IStatDefinitionRepository _statDefinitionRepository;
        private readonly IStatRepository _statRepository;
        private readonly IStatFactory _statFactory;
        #endregion

        #region Constructors
        private SqlStatsDataStore(IDatabase database)
        {
            _statFactory = Core.Stats.StatFactory.Create();
            _statRepository = StatRepository.Create(
                database,
                _statFactory);

            var statDefinitionFactory = StatDefinitionFactory.Create();
            _statDefinitionRepository = StatDefinitionRepository.Create(
                database,
                statDefinitionFactory);
        }
        #endregion

        #region Properties
        public IStatDefinitionRepository StatDefinitions { get { return _statDefinitionRepository; } }

        public IStatRepository Stats { get { return _statRepository; } }

        public IStatFactory StatFactory { get { return _statFactory; } }
        #endregion

        #region Methods
        public static IStatsDataStore Create(IDatabase database)
        {
            var dataStore = new SqlStatsDataStore(database);
            return dataStore;
        }
        #endregion
    }
}
