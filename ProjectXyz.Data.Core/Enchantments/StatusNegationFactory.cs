using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class StatusNegationFactory : IStatusNegationFactory
    {
        #region Constructors
        private StatusNegationFactory()
        {
        }
        #endregion

        #region Methods
        public static IStatusNegationFactory Create()
        {
            var factory = new StatusNegationFactory();
            return factory;
        }

        public IStatusNegation Create(
            Guid id,
            Guid statId,
            Guid enchantmentStatusId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentStatusId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStatusNegation>() != null);

            var statusNegation = StatusNegation.Create(
                id, 
                statId, 
                enchantmentStatusId);
            return statusNegation;
        }
        #endregion
    }
}
