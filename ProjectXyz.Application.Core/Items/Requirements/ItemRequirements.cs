using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Items.Requirements
{
    public sealed class ItemRequirements : IItemRequirements
    {
        #region Fields
        private readonly Guid? _raceDefinitionId;
        private readonly Guid? _classDefinitionId;
        private readonly IStatCollection _stats;
        #endregion

        #region Constructors
        private ItemRequirements(
            Guid? raceDefinitionId,
            Guid? classDefinitionId,
            IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);

            _raceDefinitionId = raceDefinitionId;
            _classDefinitionId = classDefinitionId;
            _stats = StatCollection.Create(stats);
        }
        #endregion
        
        #region Properties
        public Guid? RaceDefinitionId
        {
            get { return _raceDefinitionId; }
        }

        public Guid? ClassDefinitionId
        {
            get { return _classDefinitionId; }
        }

        public IStatCollection Stats
        {
            get { return _stats; }
        }
        #endregion

        #region Methods
        public static IItemRequirements Create(
            Guid? raceDefinitionId,
            Guid? classDefinitionId,
            IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IItemRequirements>() != null);
            
            var requirements = new ItemRequirements(
                raceDefinitionId,
                classDefinitionId,
                stats);
            return requirements;
        }
        #endregion
    }
}
