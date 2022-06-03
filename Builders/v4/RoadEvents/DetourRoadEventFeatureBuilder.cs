using System;
using System.Collections.Generic;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    public sealed class DetourRoadEventFeatureBuilder : RoadEventFeatureBuilder<DetourRoadEventFeatureBuilder, DetourRoadEvent>
    {
        public DetourRoadEventFeatureBuilder(string sourceId, string featureId, string roadName, Direction direction)
            : this(new List<Action<RoadEventFeature>>(), (feature, detour) =>
            {
                var geometry = MultiPoint.FromCoordinates(Enumerable.Empty<Position>());
                feature.Id = featureId;
                feature.Geometry = geometry;
                feature.BoundaryBox = geometry.BoundaryBox.ToList().AsReadOnly();
                feature.Properties.CoreDetails.DataSourceId = sourceId;
                feature.Properties.CoreDetails.RoadNames.Add(roadName);
                feature.Properties.CoreDetails.Direction = direction;
                detour.EventStatus = EventStatus.Pending;

                detour.EndDateAccuracy = TimeVerification.Estimated;
                detour.StartDateAccuracy = TimeVerification.Estimated;
            })
        {

        }

        private DetourRoadEventFeatureBuilder(IEnumerable<Action<RoadEventFeature>> configuration, Action<RoadEventFeature, DetourRoadEvent> step) : 
            base(configuration, step)
        {

        }

        public DetourRoadEventFeatureBuilder WithBeginning(double milepost)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.BeginningMilepost = milepost;
                workZone.BeginningCrossStreet = string.Empty;
            });
        }

        public DetourRoadEventFeatureBuilder WithBeginning(string crossStreet)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.BeginningMilepost = 0;
                workZone.BeginningCrossStreet = crossStreet;
            });
        }

        public DetourRoadEventFeatureBuilder WithEnding(double milepost)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndingMilepost = milepost;
                workZone.EndingCrossStreet = string.Empty;
            });
        }

        public DetourRoadEventFeatureBuilder WithEnding(string crossStreet)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndingMilepost = 0;
                workZone.EndingCrossStreet = crossStreet;
            });
        }

        public DetourRoadEventFeatureBuilder WithStatus(EventStatus value)
        {
            return CreateWith((_, workZone) => workZone.EventStatus = value);
        }

        public DetourRoadEventFeatureBuilder WithStart(DateTimeOffset value, TimeVerification accuracy)
        {
            return CreateWith((_, detour) =>
            {
                detour.StartDate = value;
                detour.StartDateAccuracy = accuracy;
            });
        }

        public DetourRoadEventFeatureBuilder WithEnd(DateTimeOffset value, TimeVerification accuracy)
        {
            return CreateWith((_, detour) =>
            {
                detour.EndDate = value;
                detour.EndDateAccuracy = accuracy;
            });
        }


        protected override DetourRoadEventFeatureBuilder CreateWith(Action<RoadEventFeature, DetourRoadEvent> step)
        {
            return new DetourRoadEventFeatureBuilder(Configuration, step);
        }

        protected override Func<DetourRoadEvent> ResultProperties { get; } = () => new DetourRoadEvent();

    }
}