using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemTypeGenerator
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
