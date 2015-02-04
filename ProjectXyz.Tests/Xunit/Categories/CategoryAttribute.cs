using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace ProjectXyz.Tests.Xunit.Categories
{
    public class CategoryAttribute : TraitAttribute
    {
        public CategoryAttribute(string name) 
            : base("Category", name)
        {
        }
    }
}
