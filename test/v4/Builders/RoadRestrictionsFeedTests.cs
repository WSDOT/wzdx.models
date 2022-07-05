using System;
using Wsdot.Wzdx.v4.Feeds;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
{
    public class RoadRestrictionsFeedTests : SchemaTests
    {
        public RoadRestrictionsFeedTests() :
            base(new Uri(Constants.Schema.V4.RoadRestrictionsEventFeedSchema))
        {

        }

        /* test constructor */

        [Fact]
        public void FactoryMethodCreatedResultShouldBeValid()
        {
            var feed = RoadRestrictionFeed.Create(
                Constants.DefaultPublisher,
                new[] { FeedDataSource.Create(Constants.DefaultSourceId, Constants.DefaultPublisher) },
                Version.Parse("2.0"));

            EnsureValid(feed);
        }

        [Fact]
        public void ConstructorDefaultResultNotValid()
        {
            var feed = new RoadRestrictionFeed();
            EnsureInvalid(feed);
        }
    }
}