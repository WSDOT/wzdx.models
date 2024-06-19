using System;
using System.Collections.Generic;
using System.Linq;
using Wzdx.GeoJson.Geometries;
using Wzdx.v4.Feeds;
using Wzdx.v4.RoadEvents;
using Wzdx.v4.WorkZones;
using Xunit;

namespace Wzdx.Models.Tests.v4.Builders
{
    public class RoadRestrictionEventRestrictionBuilderTests : SchemaTests
    {
        public RoadRestrictionEventRestrictionBuilderTests() :
            base(new Uri(Constants.Schema.V4.RoadRestrictionsEventFeedSchema))
        {

        }

        [Fact]
        public void DefaultShouldBeValid()
        {
            const string featureId = "test-restriction";
            EnsureResultWith(featureId, _ => _, features =>
            {
                features = features.ToList();
                Assert.Single(features);
                Assert.Equal((string)featureId, (string)features.First().Id);
            });
        }


        private void EnsureResultWith(string featureId, Func<RoadRestrictionFeatureBuilder, RoadRestrictionFeatureBuilder> setup, Action<IEnumerable<RoadEventFeature>> assertion)
        {
            var feed = RoadRestrictionFeedBuilder.Factory(Constants.DefaultPublisher)

                .Create()
                .WithSource(Constants.DefaultSourceId, sourceBuilder => sourceBuilder
                    .WithFeature(featureId, factory => setup(factory
                        .RoadRestriction(string.Empty, Direction.Northbound, MultiPoint.FromCoordinates(new[] { new Position(0, 0) }), RestrictionType.NoParking, r => r))))
                .Result();

            EnsureValid(feed);
            assertion(feed.Features);
        }
    }
}