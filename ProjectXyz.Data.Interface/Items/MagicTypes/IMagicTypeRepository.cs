using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicTypeRepository
    {
        #region Methods
        IMagicType Add(
            Guid id,
            Guid nameStringResourceId,
            int rarityWeighting);

        void RemoveById(Guid id);

        IMagicType GetById(Guid id);

        IEnumerable<IMagicType> GetAll();
        #endregion
    }
}
