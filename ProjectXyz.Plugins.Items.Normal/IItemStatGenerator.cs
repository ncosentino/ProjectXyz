using System.Collections.Generic;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public interface IItemStatGenerator
    {
        IEnumerable<IStat> GenerateItemStats(
            IRandom randomizer,
            IEnumerable<IItemDefinitionStat> itemDefinitionStats);
    }
}