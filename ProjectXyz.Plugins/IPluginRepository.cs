using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins
{
    public interface IPluginRepository<TPlugin> : IPluginRepository
    {
        #region Properties
        IEnumerable<TPlugin> Plugins { get; }
        #endregion
    }

    public interface IPluginRepository
    {
        #region Methods
        void Scan();
        #endregion
    }
}
