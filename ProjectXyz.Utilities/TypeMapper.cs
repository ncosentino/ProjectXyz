using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Utilities
{
    public sealed class TypeMapper : ITypeMapper
    {
        #region Fields
        private readonly Type[] _typeCache;
        #endregion

        #region Constructors
        private TypeMapper(IEnumerable<Type> types)
        {
            _typeCache = types
                .Where(x => !x.IsAbstract &&
                            !x.IsInterface &&
                            x.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 0) != null)
                .ToArray();
        }
        #endregion

        #region Methods
        public static ITypeMapper Create(IEnumerable<Type> types)
        {
            var mapper = new TypeMapper(types);
            return mapper;
        }

        public Type GetConcreteType(Type type)
        {
            return _typeCache.FirstOrDefault(type.IsAssignableFrom);
        }
        #endregion
    }
}
