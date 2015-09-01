using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Resources
{
    public interface IGraphicResourceRepository
    {
        #region Methods
        IGraphicResource Add(
            Guid id,
            Guid displayLanguageId,
            string name);

        IGraphicResource GetById(Guid id);

        IEnumerable<IGraphicResource> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
