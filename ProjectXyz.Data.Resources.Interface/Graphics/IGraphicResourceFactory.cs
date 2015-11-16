using System;

namespace ProjectXyz.Data.Resources.Interface.Graphics
{
    public interface IGraphicResourceFactory
    {
        #region Methods
        IGraphicResource Create(
            Guid id,
            Guid displayLanguageId,
            string value);
        #endregion
    }
}
