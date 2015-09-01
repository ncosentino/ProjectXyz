using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Materials
{
    public interface IMaterialTypeRepository
    {
        #region Methods
        IMaterialType Add(
            Guid id,
            Guid nameStringResourceId);

        void RemoveById(Guid id);

        IMaterialType GetById(Guid id);

        IEnumerable<IMaterialType> GetAll();
        #endregion
    }
}
