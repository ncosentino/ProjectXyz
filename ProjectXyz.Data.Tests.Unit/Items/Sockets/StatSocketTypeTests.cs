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
    public class StatSocketTypeTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var statDefinitionId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();

            // Execute
            var result = StatSocketType.Create(
                id,
                statDefinitionId,
                socketTypeId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(statDefinitionId, result.StatDefinitionId);
            Assert.Equal(socketTypeId, result.SocketTypeId);
        }
    }
}
