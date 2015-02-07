using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Actors
{
    public interface IActorManager
    {
        #region Methods
        IActor GetActorById(Guid actorId, IActorContext actorContext);
        #endregion
    }
}
