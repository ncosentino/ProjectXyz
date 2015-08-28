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
    public class SocketTypeFactoryTests
    {
        [Fact]
        public void Create_ValidArguments_ExpectedPropertyValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var socketTypeFactory = SocketTypeFactory.Create();

            // Execute
            var result = socketTypeFactory.Create(
                id,
                nameStringResourceId);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }
    }
}
