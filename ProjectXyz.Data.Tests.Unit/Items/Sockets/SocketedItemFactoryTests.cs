using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.Sockets
{
    [DataLayer]
    [Items]
    public class SocketedItemFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var parentItemId = Guid.NewGuid();
            var childItemId = Guid.NewGuid();

            var socketedItemFactory = SocketedItemFactory.Create();

            // Execute
            var result = socketedItemFactory.Create(
                id,
                parentItemId,
                childItemId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(parentItemId, result.ParentItemId);
            Assert.Equal(childItemId, result.ChildItemId);
        }
    }
}
