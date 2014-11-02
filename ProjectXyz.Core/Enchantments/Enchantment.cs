using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Enchantments;

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
            private set;
        }

        public double Value
        {
            get;
            private set;
        }

        public string CalculationId
        {
            get;
            private set;
        }

        public TimeSpan RemainingDuration
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IEnchantment Create()
        {
            return new Enchantment();
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            if (this.HasInfiniteDuration())
            {
                return;
            }

            RemainingDuration -= elapsedTime;
            if (RemainingDuration < TimeSpan.Zero)
            {
                RemainingDuration = TimeSpan.Zero;
            }
        }
        #endregion
    }
}
