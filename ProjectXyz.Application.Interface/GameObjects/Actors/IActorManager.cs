using System;

namespace ProjectXyz.Application.Interface.GameObjects.Actors
{
    public interface IActorManager
    {
        #region Methods
        IActor GetActorById(Guid actorId, IActorContext actorContext);
        #endregion
    }
}
