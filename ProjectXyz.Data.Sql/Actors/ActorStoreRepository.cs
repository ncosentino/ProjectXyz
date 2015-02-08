using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Actors;

namespace ProjectXyz.Data.Sql.Actors
{
    public sealed class ActorStoreRepository : IActorStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IActorStoreFactory _factory;
        #endregion

        #region Constructors
        private ActorStoreRepository(IDatabase database, IActorStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IActorStoreRepository Create(IDatabase database, IActorStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IActorStoreRepository>() != null);

            return new ActorStoreRepository(database, factory);
        }

        public IActorStore GetById(Guid actorId)
        {
            return _factory.CreateActorStore();
        }
        #endregion
    }
}
