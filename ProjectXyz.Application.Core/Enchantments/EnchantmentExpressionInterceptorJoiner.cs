using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentExpressionInterceptorJoiner : IEnchantmentExpressionInterceptorJoiner
    {
        public IEnchantmentExpressionInterceptor Join(IEnumerable<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors)
        {
            var enchantmentExpressionInterceptor = new MultiEnchantmentExpressionInterceptor(enchantmentExpressionInterceptors);
            return enchantmentExpressionInterceptor;
        }
    }
}
