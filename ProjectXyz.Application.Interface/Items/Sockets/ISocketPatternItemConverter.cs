using System.Collections.Generic;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Interface.Items.Sockets
{
    public interface ISocketPatternItemConverter
    {
        #region Methods
        IItem ConvertItem(
            IItemContext itemContext,
            IRandom randomizer,
            IItem itemToConvert,
            IEnumerable<ISocketPatternDefinition> socketPatternDefinitions);

        IItem ConvertItem(
            IItemContext itemContext,
            IRandom randomizer,
            IItem itemToConvert,
            ISocketPatternDefinition socketPatternDefinition);
        #endregion
    }
}