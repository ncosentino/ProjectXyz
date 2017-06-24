using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.States
{
    public interface IStateContextProvider :
        IReadOnlyDictionary<IIdentifier, IStateContext>,
        IComponent
    {
        
    }
}