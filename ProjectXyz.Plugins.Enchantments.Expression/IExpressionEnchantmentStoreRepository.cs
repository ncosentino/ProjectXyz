﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentStoreRepositoryContract))]
    public interface IExpressionEnchantmentStoreRepository
    {
        #region Methods
        void Add(IExpressionEnchantmentStore expressionEnchantmentStore);

        void RemoveById(Guid id);

        IExpressionEnchantmentStore GetById(Guid id);
        #endregion
    }
}