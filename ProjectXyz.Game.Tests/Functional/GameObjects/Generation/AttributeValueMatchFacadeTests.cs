using System;
using System.Collections;
using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Shared.Framework;
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
                IFilterAttributeValue value1,
                IFilterAttributeValue value2,
                bool expectedResult)
                : this(name, value1, value2, null, expectedResult)
            {
            }

            public TestScenario(
                string name,
                IFilterAttributeValue value1,
                IFilterAttributeValue value2,
                Exception exception)
                : this (name, value1, value2, exception, false)
            {
            }

            private TestScenario(
                string name,
                IFilterAttributeValue value1,
                IFilterAttributeValue value2,
                Exception exception,
                bool expectedResult)
            {
                _name = name;
                Value1 = value1;
                Value2 = value2;
                Exception = exception;
                ExpectedResult = expectedResult;
            }

            public IFilterAttributeValue Value1 { get; }

            public IFilterAttributeValue Value2 { get; }
            
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
                    "Matches True == num1",
                    new TrueAttributeFilterValue(),
                    new DoubleFilterAttributeValue(123),
                    true);
                yield return new TestScenario(
                    "No Match True == num1",
                    new FalseAttributeFilterValue(),
                    new DoubleFilterAttributeValue(123),
                    false);
                yield return new TestScenario(
                    "Throws str1 == num1",
                    new StringFilterAttributeValue("value"),
                    new DoubleFilterAttributeValue(123),
                    new InvalidOperationException("expected"));
                yield return new TestScenario(
                    "No Match str1 == Not(num1)",
                    new StringFilterAttributeValue("value"),
                    new NotFilterAttributeValue(new DoubleFilterAttributeValue(123)),
                    new InvalidOperationException("expected"));
                yield return new TestScenario(
                    "Match str1 == str2",
                    new StringFilterAttributeValue("same value"),
                    new StringFilterAttributeValue("same value"),
                    true);
                yield return new TestScenario(
                    "No Match str1 != str2",
                    new StringFilterAttributeValue("same value"),
                    new StringFilterAttributeValue("not same value"),
                    false);
                yield return new TestScenario(
                    "Match str1 == Not(str2)",
                    new StringFilterAttributeValue("same value"),
                    new NotFilterAttributeValue(new StringFilterAttributeValue("not same value")),
                    true);
                yield return new TestScenario(
                     "Match Not(str1) == str2",
                     new NotFilterAttributeValue(new StringFilterAttributeValue("same value")),
                     new StringFilterAttributeValue("not same value"),
                     true);
                yield return new TestScenario(
                    "Match num1 == num2",
                    new DoubleFilterAttributeValue(123),
                    new DoubleFilterAttributeValue(123),
                    true);
                yield return new TestScenario(
                    "No Match num1 != num2",
                    new DoubleFilterAttributeValue(123),
                    new DoubleFilterAttributeValue(456),
                    false);
                yield return new TestScenario(
                    "Match num1 == Not(num2)",
                    new DoubleFilterAttributeValue(123),
                    new NotFilterAttributeValue(new DoubleFilterAttributeValue(456)),
                    true);
                yield return new TestScenario(
                    "Match Not(num1) == num2",
                    new NotFilterAttributeValue(new DoubleFilterAttributeValue(123)),
                    new DoubleFilterAttributeValue(456),
                    true);
                yield return new TestScenario(
                    "Match str1 in (str3, str2, str1)",
                    new StringFilterAttributeValue("val1"),
                    new AnyStringCollectionFilterAttributeValue("val3", "val2", "val1"),
                    true);
                yield return new TestScenario(
                    "No Match str1 != (str3, str2, str1)",
                    new StringFilterAttributeValue("val1"),
                    new AllStringCollectionFilterAttributeValue("val3", "val2", "val1"),
                    false);
                yield return new TestScenario(
                    "Match str1 is every element of (str1)",
                    new StringFilterAttributeValue("val1"),
                    new AllStringCollectionFilterAttributeValue("val1"),
                    true);
                yield return new TestScenario(
                    "No Match str1 in (str3, str2)",
                    new StringFilterAttributeValue("val1"),
                    new AnyStringCollectionFilterAttributeValue("val3", "val2"),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) intersects (str3, str2)",
                    new AnyStringCollectionFilterAttributeValue("val1", "val2"),
                    new AnyStringCollectionFilterAttributeValue("val3", "val2"),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not intersect (str3, str4)",
                    new AnyStringCollectionFilterAttributeValue("val1", "val2"),
                    new AnyStringCollectionFilterAttributeValue("val3", "val4"),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) matches all of (str2, str1)",
                    new AllStringCollectionFilterAttributeValue("val1", "val2"),
                    new AllStringCollectionFilterAttributeValue("val2", "val1"),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not match all of (str1, str3)",
                    new AllStringCollectionFilterAttributeValue("val1", "val2"),
                    new AllStringCollectionFilterAttributeValue("val1", "val3"),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) matches all of (str2, str1)",
                    new AnyStringCollectionFilterAttributeValue("val1", "val2"),
                    new AllStringCollectionFilterAttributeValue("val2", "val1"),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not match all of (str1, str3)",
                    new AnyStringCollectionFilterAttributeValue("val1", "val2"),
                    new AllStringCollectionFilterAttributeValue("val1", "val3"),
                    false);
                yield return new TestScenario(
                    "Match str1 in Not(str3, str2)",
                    new StringFilterAttributeValue("val1"),
                    new NotFilterAttributeValue(new AnyStringCollectionFilterAttributeValue("val3", "val2")),
                    true);
                yield return new TestScenario(
                    "Match Not(str1) in (str3, str2)",
                    new NotFilterAttributeValue(new StringFilterAttributeValue("val1")),
                    new AnyStringCollectionFilterAttributeValue("val3", "val2"),
                    true);
                yield return new TestScenario(
                    "Match id1 == id2",
                    new IdentifierFilterAttributeValue(new StringIdentifier("same value")),
                    new IdentifierFilterAttributeValue(new StringIdentifier("same value")),
                    true);
                yield return new TestScenario(
                    "No Match str1 != str2",
                    new IdentifierFilterAttributeValue(new StringIdentifier("same value")),
                    new IdentifierFilterAttributeValue(new StringIdentifier("not same value")),
                    false);
                yield return new TestScenario(
                    "Match str1 == Not(str2)",
                    new IdentifierFilterAttributeValue(new StringIdentifier("same value")),
                    new NotFilterAttributeValue(new IdentifierFilterAttributeValue(new StringIdentifier("not same value"))),
                    true);
                yield return new TestScenario(
                     "Match Not(str1) == str2",
                     new NotFilterAttributeValue(new IdentifierFilterAttributeValue(new StringIdentifier("same value"))),
                     new IdentifierFilterAttributeValue(new StringIdentifier("not same value")),
                     true);
                yield return new TestScenario(
                    "Match str1 in (str3, str2, str1)",
                    new IdentifierFilterAttributeValue(new StringIdentifier("val1")),
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val3"),
                        new StringIdentifier("val2"),
                        new StringIdentifier("val1")),
                    true);
                yield return new TestScenario(
                    "No Match str1 != (str3, str2, str1)",
                    new IdentifierFilterAttributeValue(new StringIdentifier("val1")),
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val3"),
                        new StringIdentifier("val2"),
                        new StringIdentifier("val1")),
                    false);
                yield return new TestScenario(
                    "Match str1 is every element of (str1)",
                    new IdentifierFilterAttributeValue(new StringIdentifier("val1")),
                    new AllIdentifierCollectionFilterAttributeValue(new StringIdentifier("val1")),
                    true);
                yield return new TestScenario(
                    "No Match str1 in (str3, str2)",
                    new IdentifierFilterAttributeValue(new StringIdentifier("val1")),
                    new AnyIdentifierCollectionFilterAttributeValue(new StringIdentifier("val3"), new StringIdentifier("val2")),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) intersects (str3, str2)",
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val2")),
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val3"),
                        new StringIdentifier("val2")),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not intersect (str3, str4)",
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val2")),
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val3"),
                        new StringIdentifier("val4")),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) matches all of (str2, str1)",
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val2")),
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val2"),
                        new StringIdentifier("val1")),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not match all of (str1, str3)",
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val2")),
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val3")),
                    false);
                yield return new TestScenario(
                    "Match (str1, str2) matches all of (str2, str1)",
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val2")),
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val2"),
                        new StringIdentifier("val1")),
                    true);
                yield return new TestScenario(
                    "No Match (str1, str2) does not match all of (str1, str3)",
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val2")),
                    new AllIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val1"),
                        new StringIdentifier("val3")),
                    false);
                yield return new TestScenario(
                    "Match str1 in Not(str3, str2)",
                    new IdentifierFilterAttributeValue(new StringIdentifier("val1")),
                    new NotFilterAttributeValue(new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val3"),
                        new StringIdentifier("val2"))),
                    true);
                yield return new TestScenario(
                    "Match Not(str1) in (str3, str2)",
                    new NotFilterAttributeValue(new IdentifierFilterAttributeValue(new StringIdentifier("val1"))),
                    new AnyIdentifierCollectionFilterAttributeValue(
                        new StringIdentifier("val3"),
                        new StringIdentifier("val2")),
                    true);
            }
        }
    }
}
