using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using Xunit;

namespace ProjectXyz.Framework.Tests.Collections
{
    public sealed class ComponentCollectionTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IRandomNumberGenerator> _mockRandomNumberGenerator;

        public ComponentCollectionTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockRandomNumberGenerator = _mockRepository.Create<IRandomNumberGenerator>();
        }

        [ClassData(typeof(TestDataForRandom))]
        [Theory]
        private void RandomOrDefault_Source_expectedValue(
            IEnumerable<string> source,
            double randomRoll,
            string expectedValue)
        {
            if (source.Any())
            {
                _mockRandomNumberGenerator
                    .Setup(x => x.NextDouble())
                    .Returns(randomRoll);
            }

            var result = source.RandomOrDefault(_mockRandomNumberGenerator.Object);
            Assert.Equal(expectedValue, result);

            _mockRepository.VerifyAll();
        }

        [ClassData(typeof(TestDataForRandom))]
        [Theory]
        private void Random_Source_expectedValue(
            IEnumerable<string> source,
            double randomRoll,
            string expectedValue)
        {
            if (source.Any())
            {
                _mockRandomNumberGenerator
                    .Setup(x => x.NextDouble())
                    .Returns(randomRoll);

                var result = source.Random(_mockRandomNumberGenerator.Object);
                Assert.Equal(expectedValue, result);
            }
            else
            {
                Assert.Throws<InvalidOperationException>(() => source.Random(_mockRandomNumberGenerator.Object));
            }

            _mockRepository.VerifyAll();
        }

        private sealed class TestDataForRandom : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new string[0], 0, null},
                new object[] {new string[0], 0.5, null},
                new object[] {new string[0], 1, null},

                new object[] {new[] {"AAA"}, 0, "AAA"},
                new object[] {new[] {"AAA"}, 0.5, "AAA"},
                new object[] {new[] {"AAA"}, 1, "AAA"},

                new object[] {new[] {"AAA", "BBB"}, 0, "BBB"},
                new object[] {new[] {"AAA", "BBB"}, 0.5, "BBB"},
                new object[] {new[] {"AAA", "BBB"}, 1, "AAA"},

                new object[] {new[] {"AAA", "BBB", "CCC"}, 0, "CCC"},
                new object[] {new[] {"AAA", "BBB", "CCC"}, 0.5, "BBB"},
                new object[] {new[] {"AAA", "BBB", "CCC"}, 1, "AAA"},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
