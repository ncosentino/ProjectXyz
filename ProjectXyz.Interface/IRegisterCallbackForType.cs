using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Interface
{
    public interface IRegisterCallbackForType<in TType, in TCallback>
    {
        #region Methods
        void RegisterCallbackForType<TSpecificType>(TCallback callbackToRegister)
            where TSpecificType : TType;

        void RegisterCallbackForType(Type type, TCallback callbackToRegister);
        #endregion
    }
}
