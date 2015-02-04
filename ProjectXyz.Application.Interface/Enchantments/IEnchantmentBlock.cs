using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentBlock : IMutableEnchantmentCollection, IUpdateElapsedTime, INotifyCollectionChanged
    {
    }
}
