using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Testing;

using Xunit;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Tests
{
    public sealed class InMemoryAttributeFiltererFunctionalTests
    {
        private readonly IAttributeFilterer _inMemoryAttributeFilterer;

        public InMemoryAttributeFiltererFunctionalTests()
        {
            var scope = new TestLifeTimeScopeFactory().CreateScope();
            _inMemoryAttributeFilterer = scope.Resolve<IAttributeFilterer>();
        }

        [ClassData(typeof(TestData))]
        [Theory]
        private void Filter_SourceWithAttributes_Expected(
            string name,
            IReadOnlyCollection<IHasFilterAttributes> source,
            IReadOnlyCollection<IFilterAttribute> attributes,
            IReadOnlyCollection<IHasFilterAttributes> expectedResults)
        {
            var filterContext = new FilterContext(
                1,
                1,
                attributes);

            var results = _inMemoryAttributeFilterer.Filter(
                source,
                filterContext);

            Assert.Equal(expectedResults, results);
        }

        private sealed class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return CreateAnyStringCollectionAndAnyStringFilter();
                yield return CreateEmptySourceEmptyFilter();
                yield return CreateSingleComponentNoSupportedAttributesEmptyFilter();
                yield return CreateMultipleComponentNoSupportedAttributesEmptyFilter();
                yield return CreateMatchingDoubleComponent();
                yield return CreateMatchingDoubleComponentIncludesAttributeless();
                yield return CreateSupportedComponentsNotInContextNotRequired();
                yield return CreateSupportedComponentsNotInContextRequired();
                yield return CreateRequiredMatchingDoubleComponentExcludesAttributeless();
                yield return CreateInRangeComponent();
                yield return CreateInRangeComponentIncludesAttributeless();
                yield return CreateMatchInRangeComponent();
                yield return CreateMatchInRangeComponentIncludesAttributeless();
                yield return CreateOneFailedMatchWhenAllSourceAndFilterRequired();
                yield return CreateOneFailedMatchNonRequiredFilterWhenAllSourceRequired();
                yield return CreateOneFailedMatchNonSourceWhenAllSourceRequired();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private object[] CreateEmptySourceEmptyFilter()
            {
                return new object[]
                {
                    "Empty Source, Empty Attributes",
                    new IHasFilterAttributes[0],
                    new IFilterAttribute[0],
                    new IHasFilterAttributes[0],
                };
            }

            //StringCollectionFilterAttributeValue
            private object[] CreateSingleComponentNoSupportedAttributesEmptyFilter()
            {
                var expectedComponent = new HasFilterAttributes();

                return new object[]
                {
                    "Single Component, No Supported Attributes, Empty Filter",
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                    },
                    new IFilterAttribute[]
                    {

                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                    },
                };
            }

            private object[] CreateAnyStringCollectionAndAnyStringFilter()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new StringFilterAttributeValue("value"),
                        true)
                });
                var component2 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new StringFilterAttributeValue("value"),
                        true)
                });
                var expectedComponent = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new AnyStringCollectionFilterAttributeValue("match", "this wont be found"),
                        true)
                });

                return new object[]
                {
                    "Filter Matching Double Component",
                    new IHasFilterAttributes[]
                    {
                        component,
                        expectedComponent,
                        component2
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new AnyStringCollectionFilterAttributeValue("match", "try and find this if you can"),
                            true)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                    },
                };
            }

            private object[] CreateMultipleComponentNoSupportedAttributesEmptyFilter()
            {
                var expectedComponent = new HasFilterAttributes();
                var expectedComponent2 = new HasFilterAttributes();
                var expectedComponent3 = new HasFilterAttributes();

                return new object[]
                {
                    "Multiple Components, No Supported Attributes, Empty Filter",
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3,
                    },
                    new IFilterAttribute[]
                    {

                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3,
                    },
                };
            }

            private object[] CreateMatchingDoubleComponent()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(1),
                        true)
                });
                var component2 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(2),
                        true)
                });
                var expectedComponent = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        true)
                });

                return new object[]
                {
                    "Filter Matching Double Component",
                    new IHasFilterAttributes[]
                    {
                        component,
                        expectedComponent,
                        component2
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new DoubleFilterAttributeValue(3),
                            true)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                    },
                };
            }

            private object[] CreateMatchingDoubleComponentIncludesAttributeless()
            {
                var expectedComponent = new HasFilterAttributes();
                var expectedComponent2 = new HasFilterAttributes();
                var expectedComponent3 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        false)
                });

                return new object[]
                {
                    "Filter With Double Match, Includes Attributeless",
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new DoubleFilterAttributeValue(3),
                            false)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3
                    },
                };
            }

            private object[] CreateSupportedComponentsNotInContextNotRequired()
            {
                var expectedComponent = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(1),
                        false)
                });
                var expectedComponent2 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(2),
                        false)
                });
                var expectedComponent3 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        false)
                });

                return new object[]
                {
                    "Supported Components Not In Context, Not Required",
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3,
                    },
                    new IFilterAttribute[]
                    {
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3,
                    },
                };
            }

            private object[] CreateSupportedComponentsNotInContextRequired()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(1),
                        true)
                });
                var component2 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(2),
                        true)
                });
                var component3 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        true)
                });

                return new object[]
                {
                    "Supported Components Not In Context, Required",
                    new IHasFilterAttributes[]
                    {
                        component,
                        component2,
                        component3,
                    },
                    new IFilterAttribute[]
                    {
                    },
                    new IHasFilterAttributes[]
                    {
                    },
                };
            }

            private object[] CreateRequiredMatchingDoubleComponentExcludesAttributeless()
            {
                var component = new HasFilterAttributes();
                var component2 = new HasFilterAttributes();
                var expectedComponent3 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        false)
                });

                return new object[]
                {
                    "Filter With Required Double Match, Excludes Attributeless",
                    new IHasFilterAttributes[]
                    {
                        component,
                        component2,
                        expectedComponent3
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new DoubleFilterAttributeValue(3),
                            true)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent3
                    },
                };
            }

            private object[] CreateInRangeComponent()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(1),
                        true)
                });
                var expectedComponent = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(2),
                        true)
                });
                var expectedComponent2 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        true)
                });

                return new object[]
                {
                    "Filter In Range",
                    new IHasFilterAttributes[]
                    {
                        component,
                        expectedComponent,
                        expectedComponent2,
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new RangeFilterAttributeValue(2, 3),
                            true)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                    },
                };
            }

            private object[] CreateInRangeComponentIncludesAttributeless()
            {
                var expectedComponent = new HasFilterAttributes();
                var expectedComponent2 = new HasFilterAttributes();
                var expectedComponent3 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new DoubleFilterAttributeValue(3),
                        false)
                });

                return new object[]
                {
                    "Filter In Range, Includes Match Plus Attributeless",
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new RangeFilterAttributeValue(1, 4),
                            false)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3
                    },
                };
            }

            private object[] CreateMatchInRangeComponent()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new RangeFilterAttributeValue(1, 9),
                        true)
                });
                var expectedComponent = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new RangeFilterAttributeValue(5, 10),
                        true)
                });
                var expectedComponent2 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new RangeFilterAttributeValue(10, 20),
                        true)
                });

                return new object[]
                {
                    "Filter Matching In Range",
                    new IHasFilterAttributes[]
                    {
                        component,
                        expectedComponent,
                        expectedComponent2,
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new DoubleFilterAttributeValue(10),
                            true)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                    },
                };
            }

            private object[] CreateMatchInRangeComponentIncludesAttributeless()
            {
                var expectedComponent = new HasFilterAttributes();
                var expectedComponent2 = new HasFilterAttributes();
                var expectedComponent3 = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("id"),
                        new RangeFilterAttributeValue(5, 15),
                        false)
                });

                return new object[]
                {
                    "Filter Match In Range, Includes Match Plus Attributeless",
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("id"),
                            new DoubleFilterAttributeValue(10),
                            false)
                    },
                    new IHasFilterAttributes[]
                    {
                        expectedComponent,
                        expectedComponent2,
                        expectedComponent3
                    },
                };
            }

            private object[] CreateOneFailedMatchWhenAllSourceAndFilterRequired()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new StringFilterAttributeValue("magic"),
                        true),
                    new FilterAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleFilterAttributeValue(5),
                        true)
                });

                return new object[]
                {
                    "All Source & Filter Required Attributes, One Fails Match, No Results",
                    new IHasFilterAttributes[]
                    {
                        component
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("magic"),
                            true),
                        new FilterAttribute(
                            new StringIdentifier("item-level"),
                            new RangeFilterAttributeValue(10, 20),
                            true),
                    },
                    new IHasFilterAttributes[]
                    {
                    },
                };
            }

            private object[] CreateOneFailedMatchNonRequiredFilterWhenAllSourceRequired()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new StringFilterAttributeValue("magic"),
                        true),
                    new FilterAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleFilterAttributeValue(5),
                        true)
                });

                return new object[]
                {
                    "All Source Required Attributes, Non-Required Filter One Fails Match, No Result",
                    new IHasFilterAttributes[]
                    {
                        component
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("magic"),
                            true),
                        new FilterAttribute(
                            new StringIdentifier("item-level"),
                            new RangeFilterAttributeValue(10, 20),
                            false),
                    },
                    new IHasFilterAttributes[]
                    {
                    },
                };
            }

            private object[] CreateOneFailedMatchNonSourceWhenAllSourceRequired()
            {
                var component = new HasFilterAttributes(new[]
                {
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new StringFilterAttributeValue("magic"),
                        true),
                    new FilterAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleFilterAttributeValue(5),
                        false)
                });

                return new object[]
                {
                    "All Filter Required Attributes, Non-Required Source One Fails Match, No Results",
                    new IHasFilterAttributes[]
                    {
                        component
                    },
                    new IFilterAttribute[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("magic"),
                            true),
                        new FilterAttribute(
                            new StringIdentifier("item-level"),
                            new RangeFilterAttributeValue(10, 20),
                            true),
                    },
                    new IHasFilterAttributes[]
                    {
                    },
                };
            }
        }

        public sealed class HasFilterAttributes : IHasFilterAttributes
        {
            public HasFilterAttributes()
                : this(Enumerable.Empty<IFilterAttribute>())
            {
            }

            public HasFilterAttributes(IEnumerable<IFilterAttribute> supportedAttributes)
            {
                SupportedAttributes = supportedAttributes;
            }

            public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
        }
    }
}
