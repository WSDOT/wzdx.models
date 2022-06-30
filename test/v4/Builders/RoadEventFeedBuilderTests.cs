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
            var feed = RoadEventFeedBuilder.Factory(Constants.DefaultPublisher)
                .Create()
                .Result();

            EnsureValid(feed);
        }
    }
}