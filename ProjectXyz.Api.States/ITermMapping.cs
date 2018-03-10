using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.States
{
    public interface ITermMapping : IReadOnlyDictionary<IIdentifier, string>
    {
    }
}