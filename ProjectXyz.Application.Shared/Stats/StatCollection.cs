using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Shared.Stats
{
    public sealed class StatCollection :
        ReadOnlyDictionary<IIdentifier, IStat>,
        IStatCollection
    {
        private static readonly IStatCollection EMPTY = new StatCollection(Enumerable.Empty<KeyValuePair<IIdentifier, IStat>>().ToDictionary());

        public StatCollection(IDictionary<IIdentifier, IStat> dictionary)
            : base(dictionary)
        {
        }

        public static IStatCollection Empty => EMPTY;
    }
}
