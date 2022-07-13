using Wsdot.Wzdx.v4.Feeds;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
{
    public class FeedSourceTests
    {
        [Fact]
        public void DefaultShouldBeEqual()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource();


            Assert.True(expected.GetHashCode() == actual.GetHashCode());
            Assert.True(expected == actual);
        }

        [Fact]
        public void EqualityShouldCheckSourceId()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                DataSourceId = "test-source"
            };

            Assert.False(expected.GetHashCode() == actual.GetHashCode());
            Assert.False(expected == actual);
        }

        [Fact]
        public void EqualityShouldCheckOrganizationName()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                OrganizationName = "test-org"
            };

            Assert.False(expected.GetHashCode() == actual.GetHashCode());
            Assert.False(expected == actual);
        }

        [Fact]
        public void EqualityShouldCheckContactName()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                ContactName = "test-name"
            };

            Assert.False(expected.GetHashCode() == actual.GetHashCode());
            Assert.False(expected == actual);
        }


        [Fact]
        public void EqualityShouldCheckContactEmail()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                ContactEmail = "test-email"
            };

            Assert.False(expected.GetHashCode() == actual.GetHashCode());
            Assert.True(expected != actual);
        }

        [Fact]
        public void EqualityShouldCheckNull()
        {
            var expected = new FeedDataSource();
            Assert.False(expected.Equals((object)null));
        }

        [Fact]
        public void EqualityShouldCheckReference()
        {
            var expected = new FeedDataSource();
            var actual = expected;

            Assert.Equal(expected, actual);
        }

    }
}