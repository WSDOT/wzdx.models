using Wzdx.v4.Feeds;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class FeedSourceTests
    {
        [Fact]
        public void DefaultShouldBeEqual()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource();


            Assert.True((bool)(expected.GetHashCode() == actual.GetHashCode()));
            Assert.True((bool)(expected == actual));
        }

        [Fact]
        public void EqualityShouldCheckSourceId()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                DataSourceId = "test-source"
            };

            Assert.False((bool)(expected.GetHashCode() == actual.GetHashCode()));
            Assert.False((bool)(expected == actual));
        }

        [Fact]
        public void EqualityShouldCheckOrganizationName()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                OrganizationName = "test-org"
            };

            Assert.False((bool)(expected.GetHashCode() == actual.GetHashCode()));
            Assert.False((bool)(expected == actual));
        }

        [Fact]
        public void EqualityShouldCheckContactName()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                ContactName = "test-name"
            };

            Assert.False((bool)(expected.GetHashCode() == actual.GetHashCode()));
            Assert.False((bool)(expected == actual));
        }


        [Fact]
        public void EqualityShouldCheckContactEmail()
        {
            var expected = new FeedDataSource();
            var actual = new FeedDataSource()
            {
                ContactEmail = "test-email"
            };

            Assert.False((bool)(expected.GetHashCode() == actual.GetHashCode()));
            Assert.True((bool)(expected != actual));
        }

        [Fact]
        public void EqualityShouldCheckNull()
        {
            var expected = new FeedDataSource();
            Assert.False((bool)expected.Equals((object)null));
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