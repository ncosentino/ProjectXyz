﻿using System;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class InventoryItemPath
    {
        #region Properties
        public GameObjectPath OwnerPath { get; set; }

        public Guid InventoryId { get; set; }

        public int Index { get; set; }
        #endregion
    }
}
