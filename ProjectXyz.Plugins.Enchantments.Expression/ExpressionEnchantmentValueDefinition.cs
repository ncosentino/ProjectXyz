using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentValueDefinition : IExpressionEnchantmentValueDefinition
    {
        #region Constructors
        private ExpressionEnchantmentValueDefinition(
            Guid id,
            Guid enchantmentDefinitionid,
            string idForExpression,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionid != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            
            this.Id = id;
            this.EnchantmentDefinitionId = enchantmentDefinitionid;
            this.IdForExpression = idForExpression;
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id
        {
            get;
            set;
        }

        /// <inheritdoc />
        public Guid EnchantmentDefinitionId
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string IdForExpression
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double MinimumValue
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double MaximumValue
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentValueDefinition Create(
            Guid id,
            Guid enchantmentDefinitionid,
            string idForExpression,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionid != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueDefinition>() != null);

            var enchantmentDefinition = new ExpressionEnchantmentValueDefinition(
                id,
                enchantmentDefinitionid,
                idForExpression,
                minimumValue,
                maximumValue);
            return enchantmentDefinition;
        }
        #endregion
    }
}