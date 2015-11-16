using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Resources.Interface.Strings
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
