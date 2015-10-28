using System.Collections.Generic;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Plugins.Items.Magic
{
    public interface IMagicItemNamer
    {
        IEnumerable<IItemNamePart> CreateItemName(
            IEnumerable<IItemNamePart> normalItemNameParts,
            IEnumerable<IItemAffix> affixes);
    }
}