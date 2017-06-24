using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public interface IStateContextProvider :
        IReadOnlyDictionary<IIdentifier, IStateContext>,
        IComponent
    {
        
    }
}