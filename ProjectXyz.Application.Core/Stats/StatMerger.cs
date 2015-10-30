using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Stats
{
    public sealed class StatMerger : IStatMerger
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        private readonly Func<double, double, double> _valueMergeCallback;
        #endregion

        #region Constructors
        public StatMerger(
            IStatFactory statFactory,
            Func<double, double, double> valueMergeCallback)
        {
            _statFactory = statFactory;
            _valueMergeCallback = valueMergeCallback;
        }
        #endregion

        #region Methods
        public IEnumerable<IStat> MergeStats(
            IEnumerable<IStat> stats1,
            IEnumerable<IStat> stats2)
        {
            var baseMapping = stats1.ToDictionary(x => x.StatDefinitionId, x => x);
            foreach (var stat in stats2)
            {
                if (baseMapping.ContainsKey(stat.StatDefinitionId))
                {
                    baseMapping[stat.StatDefinitionId] = _statFactory.Create(
                        Guid.NewGuid(),
                        stat.StatDefinitionId,
                        baseMapping[stat.StatDefinitionId].Value + stat.Value);
                }
                else
                {
                    baseMapping[stat.StatDefinitionId] = stat;
                }
            }

            return baseMapping.Values;
        }
        #endregion
    }
}