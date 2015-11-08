using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Core.Stats;

namespace ProjectXyz.Data.Core.Actors
{
    public class ActorStore : IActorStore
    {
        #region Fields
        private readonly IMutableStatCollection _stats;
        private readonly Guid _id;
        #endregion

        #region Constructors
        private ActorStore(Guid id)
        {
            _stats = StatCollection.Create();

            _id = id;
        }
        #endregion

        #region Properties
        public IMutableStatCollection Stats => _stats;

        public Guid Id => _id;
        #endregion

        #region Methods
        public static IActorStore Create(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IActorStore>() != null);
            return new ActorStore(id);
        }
        #endregion
    }
}
