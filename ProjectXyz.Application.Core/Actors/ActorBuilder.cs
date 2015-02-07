using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;

namespace ProjectXyz.Application.Core.Actors
{
    public sealed class ActorBuilder : IActorBuilder
    {
        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="ActorBuilder"/> class from being created.
        /// </summary>
        private ActorBuilder()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new <see cref="IActorBuilder"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="IActorBuilder"/> instance.</returns>
        public static IActorBuilder Create()
        {
            Contract.Ensures(Contract.Result<IActorBuilder>() != null);

            return new ActorBuilder();
        }
        #endregion
    }
}
