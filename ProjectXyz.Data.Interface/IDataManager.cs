using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Contracts;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Maps;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Data.Resources.Interface;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IDataManagerContract))]
    public interface IDataManager
    {
        #region Properties
        IActorStoreRepository Actors { get; }

        IMapStoreRepository Maps { get; }

        IEnchantmentsDataManager Enchantments { get; }

        IStatsDataManager Stats { get; }

        IDiseaseDataManager Diseases { get; }

        IItemDataManager Items { get; }

        IWeatherDataManager Weather { get; }

        IResourcesDataManager Resources { get; }
        #endregion
    }
}
