using System;
using System.Linq;
using Wsdot.Wzdx.v4.Feeds;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
{
    public class FeedInfoBuilderTests : SchemaTests
    {
        public FeedInfoBuilderTests() :
            base(new Uri(Constants.Schema.V4.FeedInfoSchema))
        {

        }

        /* test constructor */

        [Fact]
        public void DefaultResultWithPublisherResultShouldBeValid()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).Result();
            Assert.NotNull(result);
            Assert.Equal(Constants.DefaultPublisher, result.Publisher);
            EnsureValid(result);
        }

        [Fact]
        public void DefaultResultWithPublisherAndVersionResultShouldBeValid()
        {
            const string publisher = Constants.DefaultPublisher + "~1";
            const string version = "2.1";
            var result = new FeedInfoBuilder(publisher, Version.Parse(version)).Result();
            Assert.NotNull(result);
            Assert.Equal(publisher, result.Publisher);
            Assert.Equal(version, result.Version);
            EnsureValid(result);
        }

        /* test publisher */

        [Fact]
        public void WithEmptyPublisherResultShouldBeValid()
        {
            const string publisher = "";
            var result = new FeedInfoBuilder(publisher).Result();

            Assert.NotNull(result);
            Assert.Equal(publisher, result.Publisher);
            EnsureValid(result);
        }

        [Fact]
        public void WithNullPublisherResultShouldNotBeValid()
        {
            var result = new FeedInfoBuilder(null).Result();

            Assert.NotNull(result);
            EnsureInvalid(result);
        }

        [Fact]
        public void WithPublisherResultShouldBeValid()
        {
            const string publisher = Constants.DefaultPublisher + "~1";
            const string version = "2.1";
            var result = new FeedInfoBuilder(Constants.DefaultPublisher, Version.Parse(version))
                .WithPublisher(publisher)
                .Result();

            Assert.NotNull(result);
            Assert.Equal(publisher, result.Publisher);
            Assert.Equal(version, result.Version);
            EnsureValid(result);
        }

        /* test version */

        [Fact]
        public void DefaultResultVersionResultShouldBeEmpty()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .Result();

            Assert.NotNull(result);
            Assert.Equal("4.0", result.Version);
            EnsureValid(result);
        }

        [Fact]
        public void WithVersionResultShouldBeValid()
        {
            const string version = "1.1";
            var result = new FeedInfoBuilder(Constants.DefaultPublisher, Version.Parse("2.2"))
                .WithVersion(Version.Parse(version))
                .Result();

            Assert.NotNull(result);
            Assert.Equal(version, result.Version);
            EnsureValid(result);
        }

        /* test contact */

        [Fact]
        public void DefaultResultContactNameShouldBeNull()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).Result();

            Assert.NotNull(result);
            Assert.Null(result.ContactName);
            EnsureValid(result);
        }

        [Fact]
        public void DefaultResultContactEmailShouldBeNull()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).Result();

            Assert.NotNull(result);
            Assert.Null(result.ContactEmail);
            EnsureValid(result);
        }

        [Fact]
        public void WithContactNameShouldBeValid()
        {
            const string contactName = "test-name";
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).WithContactName(contactName).Result();

            Assert.NotNull(result);
            Assert.Equal(contactName, result.ContactName);
            EnsureValid(result);
        }

        [Fact]
        public void WithContactEmailShouldBeValid()
        {
            const string contactEmail = "test-email@test.ask";
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).WithContactEmail(contactEmail).Result();

            Assert.NotNull(result);
            Assert.Equal(contactEmail, result.ContactEmail);
            EnsureValid(result);
        }

        [Fact]
        public void WithContactShouldBeValid()
        {
            const string contactName = "test-name";
            const string contactEmail = "test-email@test.ask";
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).WithContact(contactName, contactEmail)
                .Result();

            Assert.NotNull(result);
            Assert.Equal(contactName, result.ContactName);
            Assert.Equal(contactEmail, result.ContactEmail);
            EnsureValid(result);
        }

        [Fact]
        public void WithNoContactShouldBeValid()
        {
            const string contactName = "test-name";
            const string contactEmail = "test-email@test.ask";
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithContact(contactName, contactEmail)
                .WithNoContact()
                .Result();

            Assert.NotNull(result);
            Assert.Null(result.ContactName);
            Assert.Null(result.ContactEmail);
            EnsureValid(result);
        }


        /* test update date */

        [Fact]
        public void DefaultResultUpdateDateShouldBeDefault()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher, Constants.Schema.V4.DefaultVersion)
                .Result();

            Assert.NotNull(result);
            Assert.Equal(default, result.UpdateDate);
            EnsureValid(result);
        }

        [Fact]
        public void WithUpdateDateShouldBeValid()
        {
            var date = DateTimeOffset.Now;
            var result = new FeedInfoBuilder(Constants.DefaultPublisher, Constants.Schema.V4.DefaultVersion)
                .WithUpdateDate(date)
                .Result();

            Assert.NotNull(result);
            Assert.Equal(date, result.UpdateDate);
            EnsureValid(result);
        }

        /* test update frequency */

        [Fact]
        public void DefaultResultUpdateFrequencyShouldBeNull()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).Result();

            Assert.NotNull(result);
            Assert.Null(result.UpdateFrequency);
            EnsureValid(result);
        }

        [Fact]
        public void WithUpdateFrequencyShouldBeValid()
        {
            const int duration = 60;
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithUpdateFrequency(TimeSpan.FromSeconds(duration))
                .Result();

            Assert.NotNull(result);
            Assert.NotNull(result.UpdateFrequency);
            Assert.Equal(duration, result.UpdateFrequency);
            EnsureValid(result);
        }

        [Fact]
        public void WithNoUpdateFrequencyShouldBeValid()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithUpdateFrequency(TimeSpan.FromSeconds(60))
                .WithNoUpdateFrequency()
                .Result();

            Assert.NotNull(result);
            Assert.Null(result.UpdateFrequency);
            EnsureValid(result);
        }

        /* test license requirement */

        [Fact]
        public void DefaultResultLicenseShouldBeNull()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).Result();
            Assert.NotNull(result);
            Assert.Null(result.License);
            EnsureValid(result);
        }

        [Fact]
        public void WithLicenseResultShouldBeValid()
        {
            const LicenseType expected = LicenseType.HttpsCreativeCommonsOrgPublicDomainZero10;
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithLicense(expected)
                .Result();

            Assert.NotNull(result);
            Assert.Equal(expected, result.License);
            EnsureValid(result);
        }

        [Fact]
        public void WithNoLicenseResultLicenseShouldBeNull()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithLicense(LicenseType.HttpsCreativeCommonsOrgPublicDomainZero10)
                .WithNoLicense()
                .Result();

            Assert.NotNull(result);
            Assert.Null(result.License);
            EnsureValid(result);
        }

        /* test source requirement */

        [Fact]
        public void DefaultResultShouldContainDefaultSource()
        {
            // info requires at least 1 source, builder provides a single default source using publisher
            const string publisher = Constants.DefaultPublisher + "~3";
            var result = new FeedInfoBuilder(publisher).Result();
            Assert.NotNull(result);
            Assert.Equal(1, result.DataSources.Count);
            Assert.Equal(publisher, result.DataSources.First().DataSourceId);
            EnsureValid(result);
        }

        [Fact]
        public void WithSourceShouldContainSourceWithoutDefault()
        {
            // info requires at least 1 source, when provided builder does not create a default
            const string publisher = Constants.DefaultPublisher + "~4";
            const string sourceId = Constants.DefaultSourceId;
            var result = new FeedInfoBuilder(publisher)
                .WithSource(sourceId)
                .Result();
            Assert.NotNull(result);
            Assert.Equal(1, result.DataSources.Count);
            Assert.Equal(sourceId, result.DataSources.First().DataSourceId);
            EnsureValid(result);
        }

        [Fact]
        public void WithMultipleSourcesShouldContainSources()
        {
            // info requires at least 1 source, yet allows for many
            const string publisher = Constants.DefaultPublisher + "~5";
            const string sourceId = Constants.DefaultSourceId;
            const int count = 10;
            var builder = new FeedInfoBuilder(publisher);
            for (var index = 0; index < count; index++)
            {
                // maintaining builder should provided pure methods and immutability
                builder = builder.WithSource($"{sourceId}~{index}");
            }

            var result = builder.Result();
            Assert.NotNull(result);
            Assert.Equal(count, result.DataSources.Count);

            for (var index = 0; index < count; index++)
            {
                Assert.Equal($"{sourceId}~{index}", result.DataSources.ElementAt(index).DataSourceId);
            }

            EnsureValid(result);
        }
    }
}