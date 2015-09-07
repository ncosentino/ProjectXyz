﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentStatDefinition : IExpressionEnchantmentStatDefinition
    {
        #region Constructors
        private ExpressionEnchantmentStatDefinition(
            Guid id,
            Guid enchantmentDefinitionid,
            string idForExpression,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionid != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            
            this.Id = id;
            this.EnchantmentDefinitionId = enchantmentDefinitionid;
            this.IdForExpression = idForExpression;
            this.StatId = statId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid EnchantmentDefinitionId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string IdForExpression
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid StatId
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStatDefinition Create(
            Guid id,
            Guid enchantmentDefinitionid,
            string idForExpression,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionid != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatDefinition>() != null);

            var enchantmentDefinition = new ExpressionEnchantmentStatDefinition(
                id,
                enchantmentDefinitionid,
                idForExpression,
                statId);
            return enchantmentDefinition;
        }
        #endregion
    }
}