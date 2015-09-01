using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Contracts;
using ProjectXyz.Application.Interface.Items.Requirements;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemContract))]
    public interface IItem : 
        IGameObject,
        IItemMetaData,
        ISocketCandidate, 
        ISocketable, 
        IObservableDurability, 
        IEnchantable
    {
        #region Properties
        Guid ItemDefinitionId { get; }

        double Weight { get; }

        double Value { get; }

        IStatCollection Stats { get; }

        IEnumerable<Guid> EquippableSlotIds { get; }

        IItemRequirements Requirements { get; }

        IEnumerable<IItemAffix> Affixes { get; }
        #endregion
    }
}
