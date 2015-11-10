using System;

namespace ProjectXyz.Utilities
{
    public interface ITypeMapper
    {
        #region Methods
        Type GetConcreteType(Type type);
        #endregion
    }
}