using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Data.Interface.Contracts
{
    [ContractClassFor(typeof(IDataStore))]
    public abstract class IDataStoreContract : IDataStore
    {
        #region Properties
        public IEnchantmentStoreRepository Enchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentStoreRepository>() != null);
                return default(IEnchantmentStoreRepository);
            }
        }

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
        #endregion
    }
}
