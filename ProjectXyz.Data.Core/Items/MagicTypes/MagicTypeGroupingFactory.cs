using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Data.Core.Items.MagicTypes
{
    public sealed class MagicTypeGroupingFactory : IMagicTypeGroupingFactory
    {
        #region Constructors
        private MagicTypeGroupingFactory()
        {
        }
        #endregion

        #region Methods
        public static IMagicTypeGroupingFactory Create()
        {
            var factory = new MagicTypeGroupingFactory();
            return factory;
        }

        public IMagicTypeGrouping Create(
            Guid id,
            Guid groupingId,
            Guid magicTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(groupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IMagicTypeGrouping>() != null);
            
            var magicTypeGrouping = MagicTypeGrouping.Create(
                id,
                groupingId,
                magicTypeId);
            return magicTypeGrouping;
        }
        #endregion
    }
}
