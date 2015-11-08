using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.GameObjects.Actors;
using ProjectXyz.Data.Interface.Actors;

namespace ProjectXyz.Application.Core.Actors
{
    public sealed class ActorManager : IActorManager
    {
        #region Fields
        private readonly IActorStoreRepository _actorStoreRepository;
        #endregion

        #region Constructors
        private ActorManager(IActorStoreRepository actorStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(actorStoreRepository != null, "actorStoreRepository");

            _actorStoreRepository = actorStoreRepository;
        }
        #endregion

        #region Methods
        public static IActorManager Create(IActorStoreRepository actorStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(actorStoreRepository != null, "actorStoreRepository");
            Contract.Ensures(Contract.Result<IActorManager>() != null);

            return new ActorManager(actorStoreRepository);
        }

        public IActor GetActorById(Guid actorId, IActorContext actorContext)
        {
            var actorStore = _actorStoreRepository.GetById(actorId);

            return Actor.Create(
                actorContext,
                actorStore);
        }
        #endregion
    }
}
