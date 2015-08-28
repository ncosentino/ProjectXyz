using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Materials
{
    public interface IMaterialTypeRepository
    {
        #region Methods
        void Add(IMaterialType itemStore);

        void RemoveById(Guid id);

        IMaterialType GetById(Guid id);

        IEnumerable<IMaterialType> GetAll();
        #endregion
    }
}
