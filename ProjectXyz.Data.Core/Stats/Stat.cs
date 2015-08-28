using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class Stat : IStat
    {
        #region Constructors
        private Stat(
            Guid id, 
            Guid statDefinitionId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);

            Id = id;
            StatDefinitionId = statDefinitionId;
            Value = value;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }
        
        public Guid StatDefinitionId
        {
            get;
            private set;
        }

        public double Value
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IStat Create(
            Guid id,
            Guid statDefinitionId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStat>() != null);
            
            return new Stat(
                id,
                statDefinitionId,
                value);
        }
        #endregion
    }
}
