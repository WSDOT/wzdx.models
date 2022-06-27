using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wsdot.Wzdx.GeoJson.Geometries;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides an immutable builder of a v4 RoadEventFeature (Detour) class
    /// </summary>
    public class DetourRoadEventFeatureBuilder : RoadEventFeatureBuilder<DetourRoadEventFeatureBuilder, DetourRoadEvent>
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
        
        [Pure]
        public DetourRoadEventFeatureBuilder WithBeginning(double milepost)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.BeginningMilepost = milepost;
                workZone.BeginningCrossStreet = string.Empty;
            });
        }

        [Pure]
        public DetourRoadEventFeatureBuilder WithBeginning(string crossStreet)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.BeginningMilepost = 0;
                workZone.BeginningCrossStreet = crossStreet;
            });
        }

        [Pure]
        public DetourRoadEventFeatureBuilder WithEnding(double milepost)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndingMilepost = milepost;
                workZone.EndingCrossStreet = string.Empty;
            });
        }

        [Pure]
        public DetourRoadEventFeatureBuilder WithEnding(string crossStreet)
        {
            return CreateWith((_, workZone) =>
            {
                workZone.EndingMilepost = 0;
                workZone.EndingCrossStreet = crossStreet;
            });
        }

        [Pure]
        public DetourRoadEventFeatureBuilder WithStatus(EventStatus value)
        {
            return CreateWith((_, workZone) => workZone.EventStatus = value);
        }

        [Pure]
        public DetourRoadEventFeatureBuilder WithStart(DateTimeOffset value, TimeVerification accuracy)
        {
            return CreateWith((_, detour) =>
            {
                detour.StartDate = value;
                detour.StartDateAccuracy = accuracy;
            });
        }

        [Pure]
        public DetourRoadEventFeatureBuilder WithEnd(DateTimeOffset value, TimeVerification accuracy)
        {
            return CreateWith((_, detour) =>
            {
                detour.EndDate = value;
                detour.EndDateAccuracy = accuracy;
            });
        }

        [Pure]
        protected override DetourRoadEventFeatureBuilder CreateWith(Action<RoadEventFeature, DetourRoadEvent> step)
        {
            return new DetourRoadEventFeatureBuilder(Configuration, step);
        }
        
        protected override Func<DetourRoadEvent> ResultProperties { get; } = () => new DetourRoadEvent();
    }
}