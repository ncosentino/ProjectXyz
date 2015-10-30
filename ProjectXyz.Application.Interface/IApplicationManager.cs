using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface
{
    public interface IApplicationManager
    {
        #region Properties
        IActorManager Actors { get; }

        IMapManager Maps { get; }

        IStatApplicationManager Stats { get; }

        IItemApplicationManager Items { get; }

        IEnchantmentApplicationManager Enchantments { get; }
        #endregion
    }
}
