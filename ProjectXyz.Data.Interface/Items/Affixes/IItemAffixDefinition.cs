using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinition
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }

        bool IsPrefix { get;}

        int MinimumLevel { get; }
        
        int MaximumLevel { get; }
        #endregion
    }
}
