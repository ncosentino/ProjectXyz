using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace ProjectXyz.Tests.Xunit.Categories
{
    public class EnchantmentsAttribute : CategoryAttribute
    {
        public EnchantmentsAttribute() 
            : base("Enchantments")
        {
        }
    }
}
