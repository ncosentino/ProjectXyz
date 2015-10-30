using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Contracts;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Stats;

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

        IEnumerable<IStat> BaseStats { get; }
        IEnumerable<IItemNamePart> ItemNameParts { get; }

        IEnumerable<Guid> EquippableSlotIds { get; }

        IItemRequirements Requirements { get; }

        IEnumerable<IItemAffix> Affixes { get; }
        #endregion
    }
}
