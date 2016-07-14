using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Framework.Interface
{
    public interface IConvert<in T1, out T2>
    {
        T2 Convert(T1 input);
    }
}
