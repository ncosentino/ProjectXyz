using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Resources.Interface.Graphics
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
