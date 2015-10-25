using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProjectXyz.Application.Interface;

namespace ProjectXyz.Application.Core
{
    public sealed class TypeLoader : ITypeLoader
    {
        #region Constructors
        private TypeLoader()
        {
        }
        #endregion

        #region Methods
        public static ITypeLoader Create()
        {
            var loader = new TypeLoader();
            return loader;
        }

        public Type GetType(string nameOfType)
        {
            var type = Type.GetType(nameOfType);
            return type;
        }
        #endregion
    }
}
