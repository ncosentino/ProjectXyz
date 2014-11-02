﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Enchantments;
using ProjectXyz.Interface.Enchantments.ExtensionMethods;

namespace ProjectXyz.Core.Enchantments
{
    public sealed class Enchantment : IEnchantment
    {
        #region Constructors
        private Enchantment()
        {
        }
        #endregion

        #region Properties
        public string StatId
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }

        public string CalculationId
        {
            get;
            set;
        }

        public TimeSpan RemainingDuration
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IEnchantment Create()
        {
            return new Enchantment();
        }
        #endregion
    }
}
