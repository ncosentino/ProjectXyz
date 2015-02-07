using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Data.Interface.Actors;

namespace ProjectXyz.Application.Core.Actors
{
    public sealed class ActorManager : IActorManager
    {
        #region Fields
        private readonly IActorBuilder _actorBuilder;
        private readonly IActorStoreRepository _actorStoreRepository;
        #endregion

        #region Constructors
        private ActorManager(IActorBuilder actorBuilder, IActorStoreRepository actorStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(actorBuilder != null, "actorBuilder");
            Contract.Requires<ArgumentNullException>(actorStoreRepository != null, "actorStoreRepository");

            _actorBuilder = actorBuilder;
            _actorStoreRepository = actorStoreRepository;
        }
        #endregion

        #region Methods
        public static IActorManager Create(IActorBuilder actorBuilder, IActorStoreRepository actorStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(actorBuilder != null, "actorBuilder");
            Contract.Requires<ArgumentNullException>(actorStoreRepository != null, "actorStoreRepository");
            Contract.Ensures(Contract.Result<IActorManager>() != null);

            return new ActorManager(
                actorBuilder,
                actorStoreRepository);
        }

        public IActor GetActorById(Guid actorId, IActorContext actorContext)
        {
            var actorStore = _actorStoreRepository.GetById(actorId);

            return Actor.Create(
                _actorBuilder,
                actorContext,
                actorStore);
        }
        #endregion
    }
}
