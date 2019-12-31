using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.States
{
    public interface IStateContextProvider : IReadOnlyDictionary<IIdentifier, IStateContext>
    {

    }
}