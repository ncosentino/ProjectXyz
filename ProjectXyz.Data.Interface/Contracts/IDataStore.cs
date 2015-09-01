using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Maps;
using ProjectXyz.Data.Interface.Resources;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Data.Interface.Contracts
{
    [ContractClassFor(typeof(IDataStore))]
    public abstract class IDataStoreContract : IDataStore
    {
        #region Properties
        public IActorStoreRepository Actors
        {
            get
            {
                Contract.Ensures(Contract.Result<IActorStoreRepository>() != null);
                return default(IActorStoreRepository);
            }
        }

        public IMapStoreRepository Maps
        {
            get
            {
                Contract.Ensures(Contract.Result<IMapStoreRepository>() != null);
                return default(IMapStoreRepository);
            }
        }

        public IDiseaseSpreadTypeRepository DiseaseSpreadType
        {
            get
            {
                Contract.Ensures(Contract.Result<IDiseaseSpreadTypeRepository>() != null);
                return default(IDiseaseSpreadTypeRepository);
            }
        }

        public IDiseaseStatesEnchantmentsRepository DiseaseStatesEnchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IDiseaseStatesEnchantmentsRepository>() != null);
                return default(IDiseaseStatesEnchantmentsRepository);
            }
        }

        public IEnchantmentsDataStore Enchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentsDataStore>() != null);
                return default(IEnchantmentsDataStore);
            }
        }

        public IStatsDataStore Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatsDataStore>() != null);
                return default(IStatsDataStore);
            }
        }

        public IDiseaseDataStore Diseases
        {
            get
            {
                Contract.Ensures(Contract.Result<IDiseaseDataStore>() != null);
                return default(IDiseaseDataStore);
            }
        }

        public IItemDataStore Items
        {
            get
            {
                Contract.Ensures(Contract.Result<IItemDataStore>() != null);
                return default(IItemDataStore);
            }
        }

        public IWeatherDataStore Weather
        {
            get
            {
                Contract.Ensures(Contract.Result<IWeatherDataStore>() != null);
                return default(IWeatherDataStore);
            }
        }

        public IResourcesDataStore Resources
        {
            get
            {
                Contract.Ensures(Contract.Result<IResourcesDataStore>() != null);
                return default(IResourcesDataStore);
            }
        }
        #endregion
    }
}
