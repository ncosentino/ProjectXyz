using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Sql.Stats
{
    public sealed class SqlStatsDataManager : IStatsDataManager
    {
        #region Fields
        private readonly IStatDefinitionRepository _statDefinitionRepository;
        private readonly IStatRepository _statRepository;
        private readonly IStatFactory _statFactory;
        private readonly IStatCollectionFactory _statCollectionFactory;
        #endregion

        #region Constructors
        private SqlStatsDataManager(IDatabase database)
        {
            _statFactory = Core.Stats.StatFactory.Create();
            _statRepository = StatRepository.Create(
                database,
                _statFactory);

            var statDefinitionFactory = StatDefinitionFactory.Create();
            _statDefinitionRepository = StatDefinitionRepository.Create(
                database,
                statDefinitionFactory);

            _statCollectionFactory = Core.Stats.StatCollectionFactory.Create();
        }
        #endregion

        #region Properties
        public IStatDefinitionRepository StatDefinitions { get { return _statDefinitionRepository; } }

        public IStatRepository Stats { get { return _statRepository; } }

        public IStatFactory StatFactory { get { return _statFactory; } }
        
        public IStatCollectionFactory StatCollectionFactory { get { return _statCollectionFactory; } }
        #endregion

        #region Methods
        public static IStatsDataManager Create(IDatabase database)
        {
            var dataManager = new SqlStatsDataManager(database);
            return dataManager;
        }
        #endregion
    }
}
