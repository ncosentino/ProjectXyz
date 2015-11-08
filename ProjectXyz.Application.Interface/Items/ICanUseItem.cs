using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(ICanUseItemContract))]
    public interface ICanUseItem
    {
        #region Methods
        bool CanUseItem(IItem item);

        void UseItem(IItem item);
        #endregion
    }
}
