using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class MagicItemNamer : IMagicItemNamer
    {
        #region Fields
        private readonly IItemNamePartFactory _itemNamePartFactory;
        #endregion

        #region Constructors
        private MagicItemNamer(IItemNamePartFactory itemNamePartFactory)
        {
            _itemNamePartFactory = itemNamePartFactory;
        }
        #endregion

        #region Methods
        public static IMagicItemNamer Create(IItemNamePartFactory itemNamePartFactory)
        {
            var magicItemNamer = new MagicItemNamer(itemNamePartFactory);
            return magicItemNamer;
        }

        public IEnumerable<IItemNamePart> CreateItemName(
            IEnumerable<IItemNamePart> normalItemNameParts,
            IEnumerable<IItemAffix> affixes)
        {
            // give our item a nice new name based on the affixes
            var nameParts = new List<IItemNamePart>(normalItemNameParts);
            var originalNamePart = nameParts.First();

            var prefix = affixes.FirstOrDefault(x => x.Prefix);
            if (prefix != null)
            {
                nameParts.Add(_itemNamePartFactory.Create(
                    Guid.NewGuid(),
                    originalNamePart.PartId,
                    prefix.NameStringResourceId,
                    0));

                // re-insert the original name part at index 1
                nameParts.Remove(originalNamePart);
                originalNamePart = _itemNamePartFactory.Create(
                    originalNamePart.Id,
                    originalNamePart.PartId,
                    originalNamePart.NameStringResourceId,
                    1);
                nameParts.Add(originalNamePart);
            }

            var suffix = affixes.FirstOrDefault(x => !x.Prefix);
            if (suffix != null)
            {
                nameParts.Add(_itemNamePartFactory.Create(
                    Guid.NewGuid(),
                    originalNamePart.PartId,
                    suffix.NameStringResourceId,
                    originalNamePart.Order + 1));
            }

            return nameParts;
        }
        #endregion
    }
}
