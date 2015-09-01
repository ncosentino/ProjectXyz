using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Interface;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemGenerator : IRegisterCallback<Guid, GenerateItemDelegate>
    {
        #region Methods
        IItem Generate(
            IRandom randomizer,
            Guid itemDefinitionId,
            Guid magicTypeId,
            int level,
            IItemContext itemContext);
        #endregion
    }
}
