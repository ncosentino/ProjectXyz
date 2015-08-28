using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Items.Sockets;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Items.Sockets
{
    [DataLayer]
    [Items]
    public class SocketTypeTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            // Execute
            var result = SocketType.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
