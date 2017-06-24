using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.States
{
    public interface ITermMapping : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}