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
    public class RoadEventFeedDetourBuilderTests : SchemaTests
    {
        public RoadEventFeedDetourBuilderTests() :
            base(new Uri(Constants.Schema.V4.RoadEventsFeedSchema))
        {

        }

        [Fact]
        public void DefaultMultiPointShouldBeValid()
        {
            const string featureId = "test-detour";
            EnsureResultWith(featureId, factory => factory.Detour(string.Empty, Direction.Northbound, MultiPoint.FromCoordinates(new[] { new Position(0, 0) })), features =>
            {
                features = features.ToList();
                Assert.Single(features);
                Assert.Equal((string)featureId, (string)features.First().Id);
            });
        }

        [Fact]
        public void DefaultLineStringShouldBeValid()
        {
            const string featureId = "test-detour";
            EnsureResultWith(featureId, factory => factory.Detour(string.Empty, Direction.Northbound, LineString.FromCoordinates(new[] { new Position(0, 0), new Position(0, 0) })), features =>
            {
                features = features.ToList();
                Assert.Single(features);
                Assert.Equal((string)featureId, (string)features.First().Id);
            });
        }

        [Fact]
        public void WithDescriptionShouldBeValid()
        {
            const string featureId = "test-detour";
            const string value = "test description";
            EnsureResultWith(featureId, builder => builder.WithDescription(value), features =>
            {
                var feature = features.Single();
                Assert.Equal((string)value, (string)feature.Properties.CoreDetails.Description);
            });
        }

        //public T WithRelationship(Relationship value)
        //{
        //    CoreDetailConfiguration.Set(details => details.Relationship, value);
        //    return Derived();
        //}

        [Fact]
        public void WithCreatedShouldBeValid()
        {
            const string featureId = "test-detour";
            var value = new DateTimeOffset(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            
            EnsureResultWith(featureId, builder => builder.WithCreated(value), features =>
            {
                var feature = features.Single();
                Assert.Equal<DateTimeOffset?>(value, feature.Properties.CoreDetails.CreationDate);
            });
        }


        //public T WithUpdated(DateTimeOffset value)
        //{
        //    CoreDetailConfiguration.Set(details => details.UpdateDate, value.ToUniversalTime());
        //    return Derived();
        //}


        private void EnsureResultWith(string featureId, Func<DetourRoadEventFeatureBuilder, DetourRoadEventFeatureBuilder> setup, Action<IEnumerable<RoadEventFeature>> assertion)
        {
            EnsureResultWith(featureId,
                factory => setup(factory.Detour(string.Empty, Direction.Northbound, MultiPoint.FromCoordinates(new[] { new Position(0, 0) }))),
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