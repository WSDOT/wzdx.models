using System;
using System.Collections.Generic;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;
using Wzdx.v4.Devices;
using Wzdx.v4.Feeds;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class FieldDeviceFeedArrowBoardBuilderTests : SchemaTests
    {
        public FieldDeviceFeedArrowBoardBuilderTests() :
            base(new Uri(Constants.Schema.V4.FieldDevicesFeedSchema))
        {

        }

        [Fact]
        public void DefaultShouldBeValid()
        {
            const string featureId = "test-arrow-board";
            EnsureResultWith(featureId, _ => _,
                features =>
                {
                    features = features.ToList();
                    Assert.Single(features);
                    var feature = features.Single();
                    Assert.Equal((string)featureId, (string)feature.Id);

                    var properties = (ArrowBoard)feature.Properties;
                    var details = properties.CoreDetails;
                    Assert.Single<string>(details.RoadNames);
                    Assert.Null(properties.IsInTransportPosition);
                    Assert.Null(properties.IsMoving);
                    Assert.Equal(ArrowBoardPattern.Unknown, properties.Pattern);
                    //Assert.Equal(SpatialVerification.Estimated, properties.BeginningAccuracy);
                    //Assert.Null(properties.EndingCrossStreet);
                    //Assert.Null(properties.EndingMilepost);
                    //Assert.Equal(SpatialVerification.Estimated, properties.EndingAccuracy);
                    //Assert.Null(details.CreationDate);
                    //Assert.Null(details.UpdateDate);
                });
        }

        private void EnsureResultWith(string featureId, Func<ArrowBoardFeatureBuilder, ArrowBoardFeatureBuilder> setup, Action<IEnumerable<FieldDeviceFeature>> assertion)
        {
            EnsureResultWith(featureId,
                factory => setup(factory.ArrowBoard(string.Empty, Point.FromCoordinates(new[] { new Position(0, 0) }))),
                assertion);
        }

        private void EnsureResultWith(string featureId, Func<IFieldDeviceFeatureBuilderFactory, IBuilder<FieldDeviceFeature>> setup, Action<IEnumerable<FieldDeviceFeature>> assertion)
        {
            var feed = FieldDeviceFeedBuilder.Factory(Constants.DefaultPublisher)
                .Create()
                .WithSource(Constants.DefaultSourceId, sourceBuilder => sourceBuilder
                    .WithFeature(featureId, setup))
                .Result();

            EnsureValid(feed);
            assertion(feed.Features);
        }
    }

    public class FieldDeviceFeedBuilderTests : SchemaTests
    {
        public FieldDeviceFeedBuilderTests() :
            base(new Uri(Constants.Schema.V4.FieldDevicesFeedSchema))
        {

        }

        /* test constructor */

        [Fact]
        public void FactoryCreatedResultShouldBeValid()
        {
            EnsureResultWith((FieldDeviceFeed feed) => { });

        }

        /* test publisher */

        [Fact]
        public void WithPublisherShouldBeValid()
        {
            const string publisher = Constants.DefaultPublisher + "~1";
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithPublisher(publisher),
                result => { Assert.Equal(publisher, result.FeedInfo.Publisher); });
        }

        /* test version */

        [Fact]
        public void WithVersionShouldBeValid()
        {
            const string version = "4.1";
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithVersion(Version.Parse(version)),
                result => { Assert.Equal(version, result.FeedInfo.Version); });
        }

        [Fact]
        public void WithNullVersionShouldThrow()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup =>
                {
                    Assert.Throws<ArgumentNullException>(() => setup.WithVersion(null));
                    return setup;
                },
                _ => { }
            );
        }

        /* test update frequency */

        [Fact]
        public void DefaultResultUpdateFrequencyShouldBeNull()
        {
            EnsureResultWith(Constants.DefaultSourceId, result =>
            {
                Assert.NotNull(result);
                Assert.Null(result.FeedInfo.UpdateFrequency);
            });
        }

        [Fact]
        public void WithUpdateFrequencyShouldBeValid()
        {
            const int duration = 60;
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithUpdateFrequency(TimeSpan.FromSeconds(duration)),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.NotNull(result.FeedInfo.UpdateFrequency);
                    Assert.Equal<int?>(duration, result.FeedInfo.UpdateFrequency);
                });
        }

        [Fact]
        public void WithNullUpdateFrequencyShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                _ => _,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.FeedInfo.UpdateFrequency);
                });
        }

        [Fact]
        public void WithNoUpdateFrequencyShouldBeValid()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithUpdateFrequency(TimeSpan.FromSeconds(65)).WithNoUpdateFrequency(),
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Null(result.FeedInfo.UpdateFrequency);
                });
        }

        /* test update date */


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
                    Assert.Equal(value.ToUniversalTime(), result.FeedInfo.UpdateDate);
                });
        }

        /* test with info */

        [Fact]
        public void WithInfoShouldBeBuilderInstance()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithInfo(Assert.IsType<FeedInfoBuilder>),
                _ => { }
            );
        }

        /* test with source */

        [Fact]
        public void WithSourceShouldBeBuilderInstance()
        {
            EnsureResultWith(Constants.DefaultPublisher,
                setup => setup.WithSource(Constants.DefaultSourceId, Assert.IsType<FieldDeviceSourceBuilder>),
                _ => { }
            );
        }


        /* test has single default source */

        /* test has single defined source */

        /* test has multiple source */

        /* test default has no features */

        /* test defined has features */

        /* test update frequency */

        private void EnsureResultWith(Action<FieldDeviceFeed> assertion)
        {
            EnsureResultWith(Constants.DefaultPublisher, assertion);
        }

        private void EnsureResultWith(string publisher, Action<FieldDeviceFeed> assertion)
        {
            EnsureResultWith(publisher, _ => _, assertion);
        }

        private void EnsureResultWith(string publisher, Func<FieldDeviceFeedBuilder, FieldDeviceFeedBuilder> setup, Action<FieldDeviceFeed> assertion)
        {
            var feed = setup(FieldDeviceFeedBuilder.Factory(publisher).Create())
                .Result();

            EnsureValid(feed);
            assertion(feed);
        }

    }
}