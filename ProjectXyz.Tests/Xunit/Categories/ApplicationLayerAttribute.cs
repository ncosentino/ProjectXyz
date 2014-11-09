using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace ProjectXyz.Tests.Xunit.Categories
{
    public class ApplicationLayerAttribute : CategoryAttribute
    {
        public ApplicationLayerAttribute() 
            : base("Application Layer")
        {
        }
    }
}
