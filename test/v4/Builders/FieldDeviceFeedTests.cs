using System;
using Wzdx.v4.Feeds;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class FieldDeviceFeedTests : SchemaTests
    {
        public FieldDeviceFeedTests() :
            base(new Uri(Constants.Schema.V4.FieldDevicesFeedSchema))
        {

        }

        /* test constructor */

        [Fact]
        public void FactoryMethodCreatedResultShouldBeValid()
        {
            var feed = FieldDeviceFeed.Create(
                Constants.DefaultPublisher,
                new[] { FeedDataSource.Create(Constants.DefaultSourceId, Constants.DefaultPublisher) },
                Version.Parse("2.0"));

            EnsureValid(feed);
        }

        [Fact]
        public void ConstructorDefaultResultNotValid()
        {
            var feed = new FieldDeviceFeed();
            EnsureInvalid(feed);
        }
    }
}