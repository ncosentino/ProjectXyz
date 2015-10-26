using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public sealed class ItemStatGenerator : IItemStatGenerator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        #endregion

        #region Constructors
        public ItemStatGenerator(IStatFactory statFactory)
        {
            _statFactory = statFactory;
        }
        #endregion

        #region Methods
        public static IItemStatGenerator Create(IStatFactory statFactory)
        {
            var generator = new ItemStatGenerator(statFactory);
            return generator;
        }

        public IEnumerable<IStat> GenerateItemStats(
            IRandom randomizer,
            IEnumerable<IItemDefinitionStat> itemDefinitionStats)
        {
            return itemDefinitionStats
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