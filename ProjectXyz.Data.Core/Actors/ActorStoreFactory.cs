using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Actors;

namespace ProjectXyz.Data.Core.Actors
{
    public sealed class ActorStoreFactory : IActorStoreFactory
    {
        #region Constructors
        private ActorStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IActorStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IActorStoreFactory>() != null);

            return new ActorStoreFactory();
        }

        public IActorStore CreateActorStore(Guid id)
        {
            return ActorStore.Create(id);
        }
        #endregion
    }
}
