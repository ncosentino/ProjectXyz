using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Contracts;
using ProjectXyz.Data.Interface.Diseases;
using ProjectXyz.Data.Interface.Maps;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IDataStoreContract))]
    public interface IDataStore
    {
        #region Properties
        IEnchantmentStoreRepository<IAdditiveEnchantmentStore> AdditiveEnchantments { get; }

        IActorStoreRepository Actors { get; }

        IMapStoreRepository Maps { get; }

        IDiseaseSpreadTypeRepository DiseaseSpreadType { get; }

        IDiseaseStatesEnchantmentsRepository DiseaseStatesEnchantments { get; }
        #endregion
    }
}
