using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemApplicationManager
    {
        #region Properties
        IItemFactory ItemFactory { get; }

        IItemMetaDataFactory ItemMetaDataFactory { get; }

        IItemRequirementsFactory ItemRequirementsFactory { get; }

        IItemAffixFactory ItemAffixFactory { get; }

        IItemAffixGenerator ItemAffixGenerator { get; }
        #endregion
    }
}
