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
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IDataStoreContract))]
    public interface IDataStore
    {
        #region Properties
        IActorStoreRepository Actors { get; }

        IMapStoreRepository Maps { get; }

        IEnchantmentsDataStore Enchantments { get; }

        IStatsDataStore Stats { get; }

        IDiseaseDataStore Diseases { get; }

        IItemDataStore Items { get; }

        IWeatherDataStore Weather { get; }

        IResourcesDataStore Resources { get; }
        #endregion
    }
}
