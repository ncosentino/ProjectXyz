using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IStringResourceRepository
    {
        #region Methods
        IStringResource Add(
            Guid id,
            Guid displayLanguageId,
            string name);

        IStringResource GetById(Guid id);

        IEnumerable<IStringResource> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
