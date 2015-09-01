using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Core.Stats
{
    public sealed class StatDefinition : IStatDefinition
    {
        #region Constructors
        private StatDefinition(
            Guid id, 
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);

            Id = id;
            NameStringResourceId = nameStringResourceId;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }
        
        public Guid NameStringResourceId
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IStatDefinition Create(
            Guid id, 
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStatDefinition>() != null);
            
            return new StatDefinition(
                id,
                nameStringResourceId);
        }
        #endregion
    }
}
