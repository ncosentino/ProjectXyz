using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Framework.Entities.Shared
{
    public sealed class GenericComponent<T> : IComponent<T>
    {
        public GenericComponent(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
