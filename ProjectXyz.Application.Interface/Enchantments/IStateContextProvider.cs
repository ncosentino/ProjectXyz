using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IStateContextProvider : IReadOnlyDictionary<IIdentifier, IStateContext>
    {
        
    }
}