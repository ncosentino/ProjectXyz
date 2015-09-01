using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Interface
{
    public interface IRegisterCallback<in TType, in TCallback>
    {
        #region Methods
        void RegisterCallback(TType value, TCallback callbackToRegister);
        #endregion
    }
}
