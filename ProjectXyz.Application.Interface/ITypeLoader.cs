using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface
{
    public interface ITypeLoader
    {
        #region Methods
        Type GetType(string nameOfType);
        #endregion
    }
}
