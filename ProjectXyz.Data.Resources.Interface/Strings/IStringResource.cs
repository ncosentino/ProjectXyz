using System;

namespace ProjectXyz.Data.Resources.Interface.Strings
{
    public interface IStringResource
    {
        #region Properties
        Guid Id { get; }

        Guid DisplayLanguageId { get; }

        string Value { get; }
        #endregion
    }
}
