using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.Feeds;
using Wsdot.Wzdx.v4.RoadEvents;
using Wsdot.Wzdx.v4.WorkZones;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
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
                Assert.Equal(featureId, features.First().Id);
            });
        }


        private void EnsureResultWith(string featureId, Func<RoadRestrictionFeatureBuilder, RoadRestrictionFeatureBuilder> setup, Action<IEnumerable<RoadEventFeature>> assertion)
        {
            var feed = RoadRestrictionFeedBuilder.Factory(Constants.DefaultPublisher)

                .Create()
                .WithSource(Constants.DefaultSourceId, sourceBuilder => sourceBuilder
                    .WithFeature(featureId, factory => setup(factory
                        .RoadRestriction(string.Empty, Direction.Northbound, MultiPoint.FromCoordinates(new[] { new Position(0, 0) })))))
                .Result();

            EnsureValid(feed);
            assertion(feed.Features);
        }
    }
}