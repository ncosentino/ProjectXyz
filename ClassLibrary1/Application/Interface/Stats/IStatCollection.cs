using System.Collections.Generic;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Application.Interface.Stats
{
    public interface IStatCollection : IReadOnlyDictionary<IIdentifier, IStat>
    {
    }
}