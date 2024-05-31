using System;
using Wzdx.v4.Feeds;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class RoadRestrictionsFeedBuilderTests : SchemaTests
    {
        public RoadRestrictionsFeedBuilderTests() :
            base(new Uri(Constants.Schema.V4.RoadRestrictionsEventFeedSchema))
        {

        }

        /* test constructor */

        [Fact]
        public void FactoryCreatedResultShouldBeValid()
        {
            EnsureResultWith((RoadRestrictionFeed feed) => { });

        }

        /* test publisher */

        [Fact]
        public void WithPublisherShouldBeValid()
        {
            const string publisher = Constants.DefaultPublisher + "~1";
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithPublisher(publisher),
                result => { Assert.Equal((string)publisher, (string)result.FeedInfo.Publisher); });
        }

        /* test version */

        [Fact]
        public void WithVersionShouldBeValid()
        {
            const string version = "4.1";
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithVersion(Version.Parse(version)),
                result => { Assert.Equal((string)version, (string)result.FeedInfo.Version); });
        }

        [Fact]
        public void WithNullVersionShouldThrow()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup =>
                {
                    Assert.Throws<ArgumentNullException>(() => setup.WithVersion(null));
                    return setup;
                },
                _ => { }
            );
        }

        /* test update frequency */

        [Fact]
        public void DefaultResultUpdateFrequencyShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId, result =>
            {
                Assert.NotNull(result);
                Assert.Null(result.FeedInfo.UpdateFrequency);
            });
        }

        [Fact]
        public void WithUpdateFrequencyShouldBeValid()
        {
            const int duration = 60;
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithUpdateFrequency(TimeSpan.FromSeconds(duration)),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.NotNull(result.FeedInfo.UpdateFrequency);
                    Assert.Equal<int?>(duration, result.FeedInfo.UpdateFrequency);
                });
        }

        [Fact]
        public void WithNullUpdateFrequencyShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithUpdateFrequency(null),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.FeedInfo.UpdateFrequency);
                });
        }

        [Fact]
        public void WithNoUpdateFrequencyShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithUpdateFrequency(TimeSpan.FromSeconds(65)).WithNoUpdateFrequency(),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.FeedInfo.UpdateFrequency);
                });
        }

        /* test update date */


        [Fact]
        public void WithUpdateDateShouldBeUtc()
        {
            // fake datetime that is UTC-8
            var date = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var value = new DateTimeOffset(date, TimeSpan.FromHours(-8));

            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateDate(value),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(value.ToLocalTime(), result.FeedInfo.UpdateDate);
                });
        }

        /* test with info */

        [Fact]
        public void WithInfoShouldBeBuilderInstance()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithInfo(Assert.IsType<FeedInfoBuilder>), 
                _ => {} 
                );
        }

        /* test with source */
        
        [Fact]
        public void WithSourceShouldBeBuilderInstance()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithSource(Constants.DefaultSourceId, Assert.IsType<RoadRestrictionSourceBuilder>),
                _ => { }
            );
        }


        /* test has single default source */

        /* test has single defined source */

        /* test has multiple source */

        /* test default has no features */

        /* test defined has features */

        /* test update frequency */
        
        private void EnsureResultWith(Action<RoadRestrictionFeed> assertion)
        {
            EnsureResultWith(Constants.DefaultPublisher, assertion);
        }

        private void EnsureResultWith(string publisher, Action<RoadRestrictionFeed> assertion)
        {
            EnsureResultWith(publisher, _ => _, assertion);
        }

        private void EnsureResultWith(string publisher, Func<RoadRestrictionFeedBuilder, RoadRestrictionFeedBuilder> setup, Action<RoadRestrictionFeed> assertion)
        {
            var feed = setup(RoadRestrictionFeedBuilder.Factory(publisher).Create())
                .Result();

            EnsureValid(feed);
            assertion(feed);
        }

    }
}