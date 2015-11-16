using System;

namespace ProjectXyz.Data.Resources.Interface.Graphics
{
    public interface IGraphicResource
    {
        #region Properties
        Guid Id { get; }

        Guid DisplayLanguageId { get; }

        string Value { get; }
        #endregion
    }
}
