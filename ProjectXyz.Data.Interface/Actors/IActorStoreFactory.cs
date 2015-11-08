using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Actors
{
    public interface IActorStoreFactory
    {
        #region Methods
        IActorStore CreateActorStore(Guid id);
        #endregion
    }
}
