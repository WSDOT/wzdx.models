using System;
using Wsdot.Wzdx.v4.Feeds;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
{
    public class RoadEventFeedBuilderTests : SchemaTests
    {
        public RoadEventFeedBuilderTests() : 
            base(new Uri(Constants.Schema.V4.RoadEventsFeedSchema))
        {
            
        }

        [Fact]
        public void FactoryCreatedResultShouldBeValid()
        {
            var feed = RoadEventFeedBuilder.Factory(Constants.RoadEventsPublisher)
                .Create()
                .Result();

            EnsureValid(feed);
        }


        [Fact]
        public void WithUpdateFrequencyShouldBeValid()
        {
            const int expected = 60;
            var feed = RoadEventFeedBuilder.Factory(Constants.RoadEventsPublisher)
                .Create()
                .WithInfo(builder => builder.WithUpdateFrequency(TimeSpan.FromSeconds(expected)))
                .Result();

            Assert.Equal(expected, feed.FeedInfo.UpdateFrequency);
            EnsureValid(feed);
        }

        [Fact]
        public void WithoutUpdateFrequencyShouldBeValid()
        {
            
            var feed = RoadEventFeedBuilder.Factory(Constants.RoadEventsPublisher)
                .Create()
                .WithInfo(builder => builder.WithNoUpdateFrequency())
                .Result();

            Assert.Null(feed.FeedInfo.UpdateFrequency);
            EnsureValid(feed);
        }
    }
}