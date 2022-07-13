using System;
using System.Linq;
using Wsdot.Wzdx.v4.Feeds;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
{
    public class FeedSourceBuilderTests : SchemaTests
    {
        public FeedSourceBuilderTests() :
            base(new Uri(Constants.Schema.V4.FeedInfoSchema))
        {
        }

        /* test constructor */

        [Fact]
        public void DefaultResultWithSourceShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(Constants.DefaultSourceId, result.DataSourceId);
                });
        }

        /* test organization */

        [Fact]
        public void DefaultOrganizationNameShouldBeSourceId()
        {
            EnsureResultWith(Constants.DefaultSourceId + "~0",
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(result.DataSourceId, result.OrganizationName);
                });
        }

        [Fact]
        public void WithOrganizationNameShouldBeSourceId()
        {
            const string organization = Constants.DefaultSourceId + "~1";
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithOrganizationName(organization),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(organization, result.OrganizationName);
                });
        }

        /* test contact */

        [Fact]
        public void DefaultResultContactNameShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.ContactName);
                });
        }

        [Fact]
        public void DefaultResultContactEmailShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.ContactEmail);
                });
        }

        [Fact]
        public void WithContactNameShouldBeValid()
        {
            const string contactName = "test-name";
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithContactName(contactName),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(contactName, result.ContactName);
                });
        }

        [Fact]
        public void WithContactEmailShouldBeValid()
        {
            const string contactEmail = "test-email@test.ask";
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithContactEmail(contactEmail),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(contactEmail, result.ContactEmail);
                });
        }

        [Fact]
        public void WithContactShouldBeValid()
        {
            const string contactName = "test-name~1";
            const string contactEmail = "test-email~1@test.ask";
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithContact(contactName, contactEmail),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(contactName, result.ContactName);
                    Assert.Equal(contactEmail, result.ContactEmail);
                });
        }

        [Fact]
        public void WithNoContactShouldBeValid()
        {
            const string contactName = "test-name";
            const string contactEmail = "test-email@test.ask";
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithContact(contactName, contactEmail).WithNoContact(),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.ContactName);
                    Assert.Null(result.ContactEmail);
                });
        }

        /* test update frequency */

        [Fact]
        public void DefaultResultUpdateFrequencyShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId, result =>
            {
                Assert.NotNull(result);
                Assert.Null(result.UpdateFrequency);
            });
        }

        [Fact]
        public void WithUpdateFrequencyShouldBeValid()
        {
            const int duration = 60;
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateFrequency(TimeSpan.FromSeconds(duration)),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.NotNull(result.UpdateFrequency);
                    Assert.Equal(duration, result.UpdateFrequency);
                });
        }

        [Fact]
        public void WithNullUpdateFrequencyShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateFrequency(null),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.UpdateFrequency);
                });
        }

        [Fact]
        public void WithNoUpdateFrequencyShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateFrequency(TimeSpan.FromSeconds(65)).WithNoUpdateFrequency(),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.UpdateFrequency);
                });
        }

        /* test update date */

        [Fact]
        public void DefaultResultUpdateDateShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId, result =>
            {
                Assert.NotNull(result);
                Assert.Null(result.UpdateDate);
            });
        }

        [Fact]
        public void WithUpdateDateShouldBeValid()
        {
            var value = DateTimeOffset.UtcNow;
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateDate(value),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.NotNull(result.UpdateDate);
                    Assert.Equal(value, result.UpdateDate);
                });
        }
        
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
                    Assert.Equal(value.ToUniversalTime(), result.UpdateDate);
                });
        }
        
        [Fact]
        public void WithNullUpdateDateShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateDate(null),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.UpdateDate);
                });
        }

        [Fact]
        public void WithNoUpdateDateShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithUpdateDate(DateTimeOffset.UtcNow).WithNoUpdateDate(),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.UpdateDate);
                });
        }

        /* test Lrs Info */

        [Fact]
        public void DefaultResultLrsInfoShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId, result =>
            {
                Assert.NotNull(result);
                Assert.Null(result.LrsType);
                Assert.Null(result.LrsUrl);
            });
        }

        [Fact]
        public void WithLrsInfoShouldBeValid()
        {
            var type = "test-lrs";
            var uri = new Uri("https://test-lrs.test/");
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithLrsInfo(type,uri),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(type, result.LrsType);
                    Assert.Equal(uri, result.LrsUrl);
                });
        }

        [Fact]
        public void WithNullLrsInfoShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithLrsInfo(null, null),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.LrsType);
                    Assert.Null(result.LrsUrl);
                });
        }

        [Fact]
        public void WithNoLrsInfoShouldBeValid()
        {
            var type = "test-lrs~0";
            var uri = new Uri("https://test-lrs.test/");
            EnsureResultWith(Constants.DefaultSourceId,
                setup => setup.WithLrsInfo(type, uri).WithNoLrsInfo(),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.LrsType);
                    Assert.Null(result.LrsUrl);
                });
        }
        
        [Fact]
        public void FromDefaultResultShouldMatchOriginal()
        {

            var original = new FeedSourceBuilder("test-source")
                .WithNoContact()
                .WithNoUpdateDate()
                .WithNoUpdateFrequency()
                .WithNoLrsInfo()
                .Result();

            EnsureResultWith("test-source~1",
                setup => setup.From(original),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("test-source~1", result.DataSourceId);
                    Assert.Equal(original.OrganizationName, result.OrganizationName);
                    Assert.Equal(original.ContactName,result.ContactName);
                    Assert.Equal(original.ContactEmail, result.ContactEmail);
                    Assert.Equal(original.UpdateDate, result.UpdateDate);
                    Assert.Equal(original.UpdateFrequency, result.UpdateFrequency);
                    Assert.Equal(original.LrsType, result.LrsType);
                    Assert.Equal(original.LrsUrl, result.LrsUrl);
                });
        }

        [Fact]
        public void FromDefinedResultShouldMatchOriginal()
        {
            
            var original = new FeedSourceBuilder("test-source")
                .WithOrganizationName("test-org")
                .WithUpdateDate(DateTimeOffset.Now)
                .WithContact("test-name", "test@email.com")
                .WithUpdateFrequency(TimeSpan.FromHours(2))
                .WithLrsInfo("test-lrs", new Uri("https://test-lrs.test"))
                .Result();

            EnsureResultWith("test-source~1", 
                setup => setup.From(original),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("test-source~1", result.DataSourceId);
                    Assert.Equal(original.OrganizationName, result.OrganizationName);
                    Assert.Equal(original.ContactName, result.ContactName);
                    Assert.Equal(original.ContactEmail, result.ContactEmail);
                    Assert.Equal(original.UpdateDate, result.UpdateDate);
                    Assert.Equal(original.UpdateFrequency, result.UpdateFrequency);
                    Assert.Equal(original.LrsType, result.LrsType);
                    Assert.Equal(original.LrsUrl, result.LrsUrl);
                });
        }

        private void EnsureResultWith(string sourceId, Action<FeedDataSource> assertion) => EnsureResultWith(sourceId, _ => _, assertion);
        private void EnsureResultWith(string sourceId, Func<FeedSourceBuilder, FeedSourceBuilder> setup, Action<FeedDataSource> assertion)
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithSource(sourceId, setup)
                .Result();

            Assert.NotNull(result);
            EnsureValid(result);

            assertion(result.DataSources.First());
        }
    }
}