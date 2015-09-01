using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Plugins.Items.Normal
{
    public interface INormalItemGenerator
    {
        #region Methods
        IItem Generate(
            IRandom randomizer,
            Guid itemDefinitionId,
            int level,
            IItemContext itemContext);
        #endregion
    }
}
