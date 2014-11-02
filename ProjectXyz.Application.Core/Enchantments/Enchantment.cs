﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class Enchantment : IEnchantment
    {
        #region Fields
        private ProjectXyz.Interface.Enchantments.IEnchantment _enchantment;
        #endregion

        #region Constructors
        protected Enchantment(ProjectXyz.Interface.Enchantments.IEnchantment enchantment)
        {
            _enchantment = enchantment;
        }
        #endregion

        #region Properties
        public string StatId
        {
            get { return _enchantment.StatId; }
        }

        public double Value
        {
            get { return _enchantment.Value; }
        }

        public string CalculationId
        {
            get { return _enchantment.CalculationId; }
        }

        public TimeSpan RemainingDuration
        {
            get { return _enchantment.RemainingDuration; }
        }
        #endregion

        #region Methods
        public static IEnchantment CreateFrom(ProjectXyz.Interface.Enchantments.IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return new Enchantment(enchantment);
        }

        public static IEnchantment CopyFrom(IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            
            var newEnchantment = ProjectXyz.Core.Enchantments.Enchantment.Create();
            newEnchantment.StatId = enchantment.StatId;
            newEnchantment.Value = enchantment.Value;
            newEnchantment.CalculationId = enchantment.CalculationId;
            newEnchantment.RemainingDuration = enchantment.RemainingDuration;

            return new Enchantment(newEnchantment);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            if (_enchantment.HasInfiniteDuration())
            {
                return;
            }

            _enchantment.RemainingDuration = TimeSpan.FromMilliseconds(Math.Max(
                (_enchantment.RemainingDuration - elapsedTime).TotalMilliseconds,
                0));
        }
        #endregion
    }
}
