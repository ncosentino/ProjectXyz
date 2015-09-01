using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IMagicTypesRandomAffixes
    {
        #region Properties
        Guid Id { get; }

        Guid MagicTypeId { get; }

        int MinimumAffixes { get; }

        int MaximumAffixes { get; }
        #endregion
    }
}
