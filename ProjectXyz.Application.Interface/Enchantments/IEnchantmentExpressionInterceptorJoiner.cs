﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentExpressionInterceptorJoiner
    {
        IEnchantmentExpressionInterceptor Join(IEnumerable<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors);
    }
}