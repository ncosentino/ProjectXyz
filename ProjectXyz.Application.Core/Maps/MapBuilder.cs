using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Maps;

namespace ProjectXyz.Application.Core.Maps
{
    public sealed class MapBuilder : IMapBuilder
    {
        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="MapBuilder"/> class from being created.
        /// </summary>
        private MapBuilder()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new <see cref="IMapBuilder"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="IMapBuilder"/> instance.</returns>
        public static IMapBuilder Create()
        {
            Contract.Ensures(Contract.Result<IMapBuilder>() != null);

            return new MapBuilder();
        }
        #endregion
    }
}
