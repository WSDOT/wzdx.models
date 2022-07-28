using System;
using System.Collections.Generic;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson.Geometries;
using Wzdx.v4.Feeds;
using Wzdx.v4.RoadEvents;
using Wzdx.v4.WorkZones;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class RoadEventFeedWorkZoneBuilderTests : SchemaTests
    {
        public RoadEventFeedWorkZoneBuilderTests() :
            base(new Uri(Constants.Schema.V4.RoadEventsFeedSchema))
        {

        }

        [Fact]
        public void DefaultShouldBeValid()
        {
            const string featureId = "test-work-zone";
            EnsureResultWith(featureId, _ => _,
                features =>
                {
                    features = features.ToList();
                    Assert.Single(features);
                    var feature = features.Single();
                    Assert.Equal((string)featureId, (string)feature.Id);

                    var properties = (WorkZoneRoadEvent)feature.Properties;
                    var details = properties.CoreDetails;
                    Assert.Single<string>(details.RoadNames);
                    Assert.Null(properties.BeginningCrossStreet);
                    Assert.Null(properties.BeginningMilepost);
                    Assert.Equal(SpatialVerification.Estimated, properties.BeginningAccuracy);
                    Assert.Null(properties.EndingCrossStreet);
                    Assert.Null(properties.EndingMilepost);
                    Assert.Equal(SpatialVerification.Estimated, properties.EndingAccuracy);
                    Assert.Null(details.CreationDate);
                    Assert.Null(details.UpdateDate);
                });
        }

        [Fact]
        public void DefaultMultiPointShouldBeValid()
        {
            const string featureId = "test-work-zone";
            EnsureResultWith(featureId,
                factory => factory.WorkZone(string.Empty, Direction.Northbound,
                    MultiPoint.FromCoordinates(new[] { new Position(0, 0) })),
                features =>
                {
                    var feature = features.Single();
                    Assert.Equal(GeometryType.MultiPoint, feature.Geometry.Type);
                });
        }

        [Fact]
        public void DefaultLineStringNoBoundingShouldBeValid()
        {
            const string featureId = "test-work-zone";
            var geometry = LineString.FromCoordinates(new[] { new Position(0, 0), new Position(0, 1) });
            geometry.BoundaryBox = null;

            EnsureResultWith(featureId, factory => factory.WorkZone(string.Empty, Direction.Northbound, geometry),
                features =>
                {
                    var feature = features.Single();
                    Assert.Equal(GeometryType.LineString, feature.Geometry.Type);
                });
        }

        [Fact]
        public void DefaultMultiPointNoBoundingShouldBeValid()
        {
            const string featureId = "test-work-zone";
            var geometry = MultiPoint.FromCoordinates(new[] { new Position(0, 0) });
            geometry.BoundaryBox = null;

            EnsureResultWith(featureId, factory => factory.WorkZone(string.Empty, Direction.Northbound, geometry),
                features =>
                {
                    var feature = features.Single();
                    Assert.Equal(GeometryType.MultiPoint, feature.Geometry.Type);
                });
        }

        [Fact]
        public void DefaultLineStringShouldBeValid()
        {
            const string featureId = "test-work-zone";
            EnsureResultWith(featureId,
                factory => factory.WorkZone(string.Empty, Direction.Southbound,
                    LineString.FromCoordinates(new[] { new Position(0, 0), new Position(0, 0) })),
                features =>
                {
                    var feature = features.Single();
                    Assert.Equal(GeometryType.LineString, feature.Geometry.Type);
                });
        }

        [Fact]
        public void DefaultWithOneRoadNameShouldReplaceValue()
        {
            const string featureId = "test-work-zone";
            var value = "test-1";
            EnsureResultWith(featureId,
                builder => builder.WithOneRoadName(value),
                features =>
                {
                    var feature = features.Single();
                    var properties = feature.Properties;
                    var details = properties.CoreDetails;

                    Assert.Single<string>(details.RoadNames);
                    Assert.Equal(value, Enumerable.Single<string>(details.RoadNames));
                });
        }

        [Fact]
        public void DefaultWithRoadNameShouldAppendValue()
        {
            const string featureId = "test-work-zone";
            var value = "test-1";
            EnsureResultWith(featureId,
                builder => builder.WithRoadName(value),
                features =>
                {
                    var feature = features.Single();
                    var properties = feature.Properties;
                    var details = properties.CoreDetails;

                    Assert.Equal(2, details.RoadNames.Count);
                    Assert.Equal(value, Enumerable.Last<string>(details.RoadNames));
                });
        }

        [Fact]
        public void DefaultWithRoadNamesShouldReplaceAll()
        {
            const string featureId = "test-work-zone";
            var values = new[] { "test-1", "test-2", "test-3" };
            EnsureResultWith(featureId,
                builder => builder.WithRoadNames(values),
                features =>
                {
                    var feature = features.Single();
                    var properties = feature.Properties;
                    var details = properties.CoreDetails;

                    Assert.Equal<ICollection<string>>(values, details.RoadNames);
                });
        }

        [Fact]
        public void WithDescriptionShouldBeValid()
        {
            const string featureId = "test-work-zone";
            const string value = "test description";
            EnsureResultWith(featureId, builder => builder.WithDescription(value),
                features =>
                {
                    var feature = features.Single();
                    var properties = feature.Properties;
                    var details = properties.CoreDetails;

                    Assert.Equal((string)value, (string)details.Description);
                });
        }

        [Fact]
        public void WithBeginningMilepostShouldBeValid()
        {
            const string featureId = "test-work-zone";
            const double value = 12.3;

            EnsureResultWith(featureId, builder => builder.WithBeginning(value, SpatialVerification.Verified),
                features =>
                {
                    var feature = features.Single();
                    var properties = (WorkZoneRoadEvent)feature.Properties;

                    Assert.NotNull(properties.BeginningMilepost);
                    Assert.Null(properties.BeginningCrossStreet);
                    Assert.Equal(value, properties.BeginningMilepost.Value);
                    Assert.Equal(SpatialVerification.Verified, properties.BeginningAccuracy);
                });
        }


        [Fact]
        public void WithEndingMilepostShouldBeValid()
        {
            const string featureId = "test-work-zone";
            const double value = 12.3;

            EnsureResultWith(featureId, builder => builder.WithEnding(value, SpatialVerification.Verified),
                features =>
                {
                    var feature = features.Single();
                    var properties = (WorkZoneRoadEvent)feature.Properties;

                    Assert.NotNull(properties.EndingMilepost);
                    Assert.Null(properties.EndingCrossStreet);
                    Assert.Equal(value, properties.EndingMilepost.Value);
                    Assert.Equal(SpatialVerification.Verified, properties.EndingAccuracy);
                });
        }


        [Fact]
        public void WithBeginningCrossStreetShouldBeValid()
        {
            const string featureId = "test-work-zone";
            const string value = "test-street";

            EnsureResultWith(featureId, builder => builder.WithBeginning(value, SpatialVerification.Verified),
                features =>
                {
                    var feature = features.Single();
                    var properties = (WorkZoneRoadEvent)feature.Properties;

                    Assert.NotNull(properties.BeginningCrossStreet);
                    Assert.Null(properties.BeginningMilepost);
                    Assert.Equal((string)value, (string)properties.BeginningCrossStreet);
                    Assert.Equal(SpatialVerification.Verified, properties.BeginningAccuracy);
                });
        }


        [Fact]
        public void WithEndingCrossStreetShouldBeValid()
        {
            const string featureId = "test-work-zone";
            const string value = "test-street";

            EnsureResultWith(featureId, builder => builder.WithEnding(value, SpatialVerification.Verified),
                features =>
                {
                    var feature = features.Single();
                    var properties = (WorkZoneRoadEvent)feature.Properties;

                    Assert.NotNull(properties.EndingCrossStreet);
                    Assert.Null(properties.EndingMilepost);
                    Assert.Equal((string)value, (string)properties.EndingCrossStreet);
                    Assert.Equal(SpatialVerification.Verified, properties.EndingAccuracy);
                });
        }

        [Fact]
        public void WithBeginningShouldBeValid()
        {
            const string featureId = "test-work-zone";

            EnsureResultWith(featureId, builder => builder.WithBeginning(SpatialVerification.Verified),
                features =>
                {
                    var feature = features.Single();
                    var properties = (WorkZoneRoadEvent)feature.Properties;
                    Assert.Equal(SpatialVerification.Verified, properties.BeginningAccuracy);
                });
        }


        [Fact]
        public void WithEndingShouldBeValid()
        {
            const string featureId = "test-work-zone";

            EnsureResultWith(featureId, builder => builder.WithEnding(SpatialVerification.Verified),
                features =>
                {
                    var feature = features.Single();
                    var properties = (WorkZoneRoadEvent)feature.Properties;
                    Assert.Equal(SpatialVerification.Verified, properties.EndingAccuracy);
                });
        }

        [Fact]
        public void WithCreatedShouldBeValid()
        {
            const string featureId = "test-work-zone";
            var value = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(-8));

            EnsureResultWith(featureId, 
                builder => builder.WithCreated(value), 
                features =>
                {
                    var feature = features.Single();
                    var properties = feature.Properties;
                    var details = properties.CoreDetails;

                    Assert.NotNull(details.CreationDate);
                    Assert.Equal<DateTimeOffset?>(value, details.CreationDate);
                    Assert.Equal(TimeSpan.Zero, details.CreationDate.Value.Offset);
                });
        }

        [Fact]
        public void WithUpdatedShouldBeValid()
        {
            const string featureId = "test-work-zone";
            var value = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(-8));

            EnsureResultWith(featureId,
                builder => builder.WithUpdated(value),
                features =>
                {
                    var feature = features.Single();
                    var properties = feature.Properties;
                    var details = properties.CoreDetails;

                    Assert.NotNull(details.UpdateDate);
                    Assert.Equal<DateTimeOffset?>(value, details.UpdateDate);
                    Assert.Equal(TimeSpan.Zero, details.UpdateDate.Value.Offset);
                });
        }

        private void EnsureResultWith(string featureId, Func<WorkZoneRoadEventFeatureBuilder, WorkZoneRoadEventFeatureBuilder> setup, Action<IEnumerable<RoadEventFeature>> assertion)
        {
            EnsureResultWith(featureId,
                factory => setup(factory.WorkZone(string.Empty, Direction.Northbound, MultiPoint.FromCoordinates(new[] { new Position(0, 0) }))),
                assertion);
        }

        private void EnsureResultWith(string featureId, Func<IRoadEventFeatureBuilderFactory, IBuilder<RoadEventFeature>> setup, Action<IEnumerable<RoadEventFeature>> assertion)
        {
            var feed = RoadEventFeedBuilder.Factory(Constants.DefaultPublisher)
                .Create()
                .WithSource(Constants.DefaultSourceId, sourceBuilder => sourceBuilder
                    .WithFeature(featureId, setup))
                .Result();

            EnsureValid(feed);
            assertion(feed.Features);
        }

    }
}