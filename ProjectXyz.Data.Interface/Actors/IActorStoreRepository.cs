using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Actors
{
    public interface IActorStoreRepository
    {
        #region Methods
        IActorStore GetById(Guid actorId);
        #endregion
    }
}
