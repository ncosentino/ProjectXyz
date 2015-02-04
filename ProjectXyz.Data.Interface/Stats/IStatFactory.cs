using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats.Contracts;

namespace ProjectXyz.Data.Interface.Stats
{
    [ContractClass(typeof(IStatFactoryContract))]
    public interface IStatFactory
    {
        #region Methods
        IStat CreateStat(Guid id, double value);
        #endregion
    }
}
