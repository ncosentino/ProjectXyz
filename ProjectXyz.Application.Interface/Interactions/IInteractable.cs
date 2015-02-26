using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Actors;

namespace ProjectXyz.Application.Interface.Interactions
{
    public interface IInteractable
    {
        #region Methods
        IEnumerable<IInteraction> GetPossibleInteractions(IInteractionContext interactionContext, IActor actor);
        #endregion
    }
}
