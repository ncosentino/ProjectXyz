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
    [ContractClassFor(typeof(IDataManager))]
    public abstract class IDataManagerContract : IDataManager
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

        public IEnchantmentsDataManager Enchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentsDataManager>() != null);
                return default(IEnchantmentsDataManager);
            }
        }

        public IStatsDataManager Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatsDataManager>() != null);
                return default(IStatsDataManager);
            }
        }

        public IDiseaseDataManager Diseases
        {
            get
            {
                Contract.Ensures(Contract.Result<IDiseaseDataManager>() != null);
                return default(IDiseaseDataManager);
            }
        }

        public IItemDataManager Items
        {
            get
            {
                Contract.Ensures(Contract.Result<IItemDataManager>() != null);
                return default(IItemDataManager);
            }
        }

        public IWeatherDataManager Weather
        {
            get
            {
                Contract.Ensures(Contract.Result<IWeatherDataManager>() != null);
                return default(IWeatherDataManager);
            }
        }

        public IResourcesDataManager Resources
        {
            get
            {
                Contract.Ensures(Contract.Result<IResourcesDataManager>() != null);
                return default(IResourcesDataManager);
            }
        }
        #endregion
    }
}
