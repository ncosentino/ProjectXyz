using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.GameObjects.Actors;

namespace ProjectXyz.Application.Interface.Interactions
{
    public interface IInteraction
    {
        #region Methods
        void Interact(IInteractionContext interactionContext, IActor actor);

        bool CanInteract(IInteractionContext interactionContext, IActor actor);
        #endregion
    }
}
