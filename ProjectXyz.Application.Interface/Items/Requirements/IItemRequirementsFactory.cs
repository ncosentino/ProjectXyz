using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Requirements
{
    public interface IItemRequirementsFactory
    {
        #region Methods
        IItemRequirements Create(
            Guid? raceDefinitionId,
            Guid? classDefinitionId,
            IEnumerable<IStat> stats);
        #endregion
    }
}
