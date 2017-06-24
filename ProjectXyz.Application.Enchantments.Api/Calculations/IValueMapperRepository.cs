using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Enchantments.Api.Calculations
{
    public interface IValueMapperRepository : IComponent
    {
        IEnumerable<ValueMapperDelegate> GetValueMappers();
    }
}
