using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Stats
{
    public sealed class StatGenerator : IStatGenerator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        #endregion

        #region Constructors
        private StatGenerator(IStatFactory statFactory)
        {
            _statFactory = statFactory;
        }
        #endregion

        #region Methods
        public static IStatGenerator Create(IStatFactory statFactory)
        {
            var generator = new StatGenerator(statFactory);
            return generator;
        }

        public IEnumerable<IStat> GenerateStats(
            IRandom randomizer,
            IEnumerable<IStatRange> statRanges)
        {
            return statRanges
                .Select(x =>
                {
                    var value =
                        x.MinimumValue +
                        randomizer.NextDouble() * (x.MaximumValue - x.MinimumValue);
                    var stat = _statFactory.Create(
                        Guid.NewGuid(),
                        x.StatDefinitionId,
                        value);
                    return stat;
                });
        }
        #endregion
    }
}