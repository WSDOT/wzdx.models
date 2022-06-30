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
        
        [Fact]
        public void ConstructedWithPublisherResultShouldBeValid()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher).Result();
            Assert.NotNull(result);
            Assert.Equal(Constants.DefaultPublisher, result.Publisher);
            EnsureValid(result);
        }

        [Fact]
        public void ConstructedWithPublisherAndVersionResultShouldBeValid()
        {
            const string publisher = Constants.DefaultPublisher + "~1";
            const string version = "2.1";
            var result = new FeedInfoBuilder(publisher, Version.Parse(version)).Result();
            Assert.NotNull(result);
            Assert.Equal(publisher, result.Publisher);
            Assert.Equal(version, result.Version);
            EnsureValid(result);
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
        
        [Fact]
        public void DefaultResultLicenseShouldBeNull()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .Result();

            Assert.NotNull(result);
            Assert.Null(result.License);
            EnsureValid(result);
        }

        [Fact] 
        public void WithLicenseResultShouldBeValid()
        {
            var result = new FeedInfoBuilder(Constants.DefaultPublisher)
                .WithLicense(LicenseType.HttpsCreativeCommonsOrgPublicDomainZero10)
                .Result();

            Assert.NotNull(result);
            Assert.Equal(LicenseType.HttpsCreativeCommonsOrgPublicDomainZero10, result.License);
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
        
        [Fact]
        public void ConstructedResultShouldContainDefaultSource()
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
            const string publisher = Constants.DefaultPublisher + "~4" ;
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