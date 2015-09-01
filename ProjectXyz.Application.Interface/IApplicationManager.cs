using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;

namespace ProjectXyz.Application.Interface
{
    public interface IApplicationManager
    {
        #region Properties
        IActorManager Actors { get; }

        IMapManager Maps { get; }

        IItemApplicationManager Items { get; }

        IEnchantmentApplicationManager Enchantments { get; }
        #endregion
    }
}
