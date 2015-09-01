using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class StatDefinitionFactory : IStatDefinitionFactory
    {
        #region Constructors
        private StatDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IStatDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IStatDefinitionFactory>() != null);
            return new StatDefinitionFactory();
        }

        public IStatDefinition Create(
            Guid id, 
            Guid nameStringResourceId)
        {
            return StatDefinition.Create(
                id,
                nameStringResourceId);
        }
        #endregion
    }
}
