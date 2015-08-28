using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicTypeRepository
    {
        #region Methods
        void Add(IMagicType itemStore);

        void RemoveById(Guid id);

        IMagicType GetById(Guid id);

        IEnumerable<IMagicType> GetAll();
        #endregion
    }
}
