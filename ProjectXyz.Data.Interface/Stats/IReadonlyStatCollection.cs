using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IReadonlyStatCollection<TStat> : IStatCollection<TStat> where TStat : IStat
    {
    }
}
