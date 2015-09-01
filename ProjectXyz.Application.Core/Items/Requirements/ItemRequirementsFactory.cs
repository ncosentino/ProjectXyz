using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Items.Requirements
{
    public sealed class ItemRequirementsFactory : IItemRequirementsFactory
    {
        #region Constructors
        private ItemRequirementsFactory()
        {
        }
        #endregion
        
        #region Methods
        public static IItemRequirementsFactory Create()
        {
            var factory = new ItemRequirementsFactory();
            return factory;
        }

        public IItemRequirements Create(
            Guid? raceDefinitionId,
            Guid? classDefinitionId,
            IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IItemRequirements>() != null);
            
            var requirements = ItemRequirements.Create(
                raceDefinitionId,
                classDefinitionId,
                stats);
            return requirements;
        }
        #endregion
    }
}
