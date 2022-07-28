using System;
using Wzdx.v4.Feeds;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class RoadEventFeedTests : SchemaTests
    {
        public RoadEventFeedTests() :
            base(new Uri(Constants.Schema.V4.RoadEventsFeedSchema))
        {

        }

        /* test constructor */

        [Fact]
        public void FactoryMethodCreatedResultShouldBeValid()
        {
            var feed = RoadEventsFeed.Create(
                Constants.DefaultPublisher,
                new[] { FeedDataSource.Create(Constants.DefaultSourceId, Constants.DefaultPublisher) },
                Version.Parse("2.0"));

            EnsureValid(feed);
        }

        [Fact]
        public void ConstructorDefaultResultNotValid()
        {
            var feed = new RoadEventsFeed();
            EnsureInvalid(feed);
        }
    }
}