using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class StatFactory : IStatFactory
    {
        #region Constructors
        private StatFactory()
        {
        }
        #endregion

        #region Methods
        public static IStatFactory Create()
        {
            Contract.Ensures(Contract.Result<IStatFactory>() != null);
            return new StatFactory();
        }

        public IStat Create(
            Guid id, 
            Guid statDefinitionId,
            double value)
        {
            return Stat.Create(
                id, 
                statDefinitionId,
                value);
        }
        #endregion
    }
}
