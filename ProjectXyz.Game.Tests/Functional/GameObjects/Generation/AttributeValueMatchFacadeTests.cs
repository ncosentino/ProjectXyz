using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Generation
{
    public sealed class AttributeValueMatchFacadeTests
    {
        private static readonly IAttributeValueMatchFacade _attributeValueMatchFacade;


        static AttributeValueMatchFacadeTests()
        {
            _attributeValueMatchFacade = CachedDependencyLoader.LifeTimeScope.Resolve<IAttributeValueMatchFacade>();
        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void Match_Scenario(TestScenario scenario)
        {
            Func<bool> testMethod = () => _attributeValueMatchFacade.Match(
                scenario.Value1,
                scenario.Value2);

            if (scenario.Exception != null)
            {
                var exception = Assert.Throws(
                    scenario.Exception.GetType(),
                    () => testMethod.Invoke());
                // TODO: if we care enough...
                ////Assert.Equal(
                ////    scenario.Exception.Message,
                ////    exception.Message);
            }
            else
            {
                var result = testMethod.Invoke();
                Assert.Equal(
                    scenario.ExpectedResult,
                    result);
            }
        }

        private sealed class TestScenario
        {
            private readonly string _name;

            public TestScenario(
                string name,
                IGeneratorAttributeValue value1,
                IGeneratorAttributeValue value2,
                bool expectedResult)
                : this(name, value1, value2, null, expectedResult)
            {
            }

            public TestScenario(
                string name,
                IGeneratorAttributeValue value1,
                IGeneratorAttributeValue value2,
                Exception exception)
                : this (name, value1, value2, exception, false)
            {
            }

            private TestScenario(
                string name,
                IGeneratorAttributeValue value1,
                IGeneratorAttributeValue value2,
                Exception exception,
                bool expectedResult)
            {
                _name = name;
                Value1 = value1;
                Value2 = value2;
                Exception = exception;
                ExpectedResult = expectedResult;
            }

            public IGeneratorAttributeValue Value1 { get; }

            public IGeneratorAttributeValue Value2 { get; }
            
            public Exception Exception { get; }

            public bool ExpectedResult { get; }

            public override string ToString() => _name;
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                foreach (var s in Generate())
                {
                    yield return new object[] { s };
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private IEnumerable<TestScenario> Generate()
            {
                yield return new TestScenario(
                    "Throws str1 == num1",
                    new StringGeneratorAttributeValue("value"),
                    new DoubleGeneratorAttributeValue(123),
                    new InvalidOperationException("expected"));
                yield return new TestScenario(
                    "No Match str1 == Not(num1)",
                    new StringGeneratorAttributeValue("value"),
                    new NotGeneratorAttributeValue(new DoubleGeneratorAttributeValue(123)),
                    new InvalidOperationException("expected"));
                yield return new TestScenario(
                    "Match str1 == str2",
                    new StringGeneratorAttributeValue("same value"),
                    new StringGeneratorAttributeValue("same value"),
                    true);
                yield return new TestScenario(
                    "No Match str1 != str2",
                    new StringGeneratorAttributeValue("same value"),
                    new StringGeneratorAttributeValue("not same value"),
                    false);
                yield return new TestScenario(
                    "Match str1 == Not(str2)",
                    new StringGeneratorAttributeValue("same value"),
                    new NotGeneratorAttributeValue(new StringGeneratorAttributeValue("not same value")),
                    true);
                yield return new TestScenario(
                     "Match Not(str1) == str2",
                     new NotGeneratorAttributeValue(new StringGeneratorAttributeValue("same value")),
                     new StringGeneratorAttributeValue("not same value"),
                     true);
                yield return new TestScenario(
                    "Match num1 == num2",
                    new DoubleGeneratorAttributeValue(123),
                    new DoubleGeneratorAttributeValue(123),
                    true);
                yield return new TestScenario(
                    "No Match num1 != num2",
                    new DoubleGeneratorAttributeValue(123),
                    new DoubleGeneratorAttributeValue(456),
                    false);
                yield return new TestScenario(
                    "Match num1 == Not(num2)",
                    new DoubleGeneratorAttributeValue(123),
                    new NotGeneratorAttributeValue(new DoubleGeneratorAttributeValue(456)),
                    true);
                yield return new TestScenario(
                    "Match Not(num1) == num2",
                    new NotGeneratorAttributeValue(new DoubleGeneratorAttributeValue(123)),
                    new DoubleGeneratorAttributeValue(456),
                    true);
                yield return new TestScenario(
                    "Match str1 in (str3, str2, str1)",
                    new StringGeneratorAttributeValue("val1"),
                    new AnyStringCollectionGeneratorAttributeValue("val3", "val2", "val1"),
                    true);
                yield return new TestScenario(
                    "No Match str1 != (str3, str2, str1)",
                    new StringGeneratorAttributeValue("val1"),
                    new AllStringCollectionGeneratorAttributeValue("val3", "val2", "val1"),
                    false);
                yield return new TestScenario(
                    "Match str1 is every element of (str1)",
                    new StringGeneratorAttributeValue("val1"),
                    new AllStringCollectionGeneratorAttributeValue("val1"),
                    true);
                yield return new TestScenario(
                    "No Match str1 in (str3, str2)",
                    new StringGeneratorAttributeValue("val1"),
                    new AnyStringCollectionGeneratorAttributeValue("val3", "val2"),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) intersects (str3, str2)",
                    new AnyStringCollectionGeneratorAttributeValue("val1", "val2"),
                    new AnyStringCollectionGeneratorAttributeValue("val3", "val2"),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not intersect (str3, str4)",
                    new AnyStringCollectionGeneratorAttributeValue("val1", "val2"),
                    new AnyStringCollectionGeneratorAttributeValue("val3", "val4"),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) matches all of (str2, str1)",
                    new AllStringCollectionGeneratorAttributeValue("val1", "val2"),
                    new AllStringCollectionGeneratorAttributeValue("val2", "val1"),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not match all of (str1, str3)",
                    new AllStringCollectionGeneratorAttributeValue("val1", "val2"),
                    new AllStringCollectionGeneratorAttributeValue("val1", "val3"),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) matches all of (str2, str1)",
                    new AnyStringCollectionGeneratorAttributeValue("val1", "val2"),
                    new AllStringCollectionGeneratorAttributeValue("val2", "val1"),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not match all of (str1, str3)",
                    new AnyStringCollectionGeneratorAttributeValue("val1", "val2"),
                    new AllStringCollectionGeneratorAttributeValue("val1", "val3"),
                    false);
                yield return new TestScenario(
                    "Match str1 in Not(str3, str2)",
                    new StringGeneratorAttributeValue("val1"),
                    new NotGeneratorAttributeValue(new AnyStringCollectionGeneratorAttributeValue("val3", "val2")),
                    true);
                yield return new TestScenario(
                    "Match Not(str1) in (str3, str2)",
                    new NotGeneratorAttributeValue(new StringGeneratorAttributeValue("val1")),
                    new AnyStringCollectionGeneratorAttributeValue("val3", "val2"),
                    true);
                yield return new TestScenario(
                    "Match id1 == id2",
                    new IdentifierGeneratorAttributeValue(new StringIdentifier("same value")),
                    new IdentifierGeneratorAttributeValue(new StringIdentifier("same value")),
                    true);
                yield return new TestScenario(
                    "No Match str1 != str2",
                    new IdentifierGeneratorAttributeValue(new StringIdentifier("same value")),
                    new IdentifierGeneratorAttributeValue(new StringIdentifier("not same value")),
                    false);
                yield return new TestScenario(
                    "Match str1 == Not(str2)",
                    new IdentifierGeneratorAttributeValue(new StringIdentifier("same value")),
                    new NotGeneratorAttributeValue(new IdentifierGeneratorAttributeValue(new StringIdentifier("not same value"))),
                    true);
                yield return new TestScenario(
                     "Match Not(str1) == str2",
                     new NotGeneratorAttributeValue(new IdentifierGeneratorAttributeValue(new StringIdentifier("same value"))),
                     new IdentifierGeneratorAttributeValue(new StringIdentifier("not same value")),
                     true);
            }
        }
    }
}
