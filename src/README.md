<img src="https://github.com/usdot-jpo-ode/wzdx/blob/main/images/wzdx_logo_blue_orange_x.png" height="150"/>

# Work Zone Data Exchange (WZDx) Specification
The [Work Zone Data Exchange (WZDx) Specification](https://github.com/usdot-jpo-ode/wzdx) aims to make harmonized work zone data provided by infrastructure owners and operators (IOOs) available for third party use, making travel on public roads safer and more efficient through ubiquitous access to data on work zone activity.

The goal of WZDx is to enable widespread access to up-to-date information about dynamic conditions occurring on roads such as construction events. Currently, many IOOs maintain data on work zone activity. However, a lack of common data standards and convening mechanisms makes it difficult and costly for third parties such as original equipment manufacturers (OEMs) and navigation applications to access and use these data across various jurisdictions. WZDx defines a common language for describing work zone information. This simplifies the design process for producers and the processing logic for consumers and makes work zone data more accessible.

Specifically, WZDx defines the structure and content of several [GeoJSON](https://datatracker.ietf.org/doc/html/rfc7946) documents that are each intended to be distributed as a data feed. The feeds describe a variety of high-level road work-related information such as the location and status of work zones, detours, and field devices.

## WZDX Models
This project contains various classes and builders used to build WZDx feeds. 

## Namespaces
---
### GeoJson
* Converters
* Geometries
---
### v3
* Feeds
* WorkZones (RoadEvents)
---
### v4
* Feeds
* Devices
* WorkZones (RoadEvents, e.g., Detours, Restrictions, WorkZones )

### Builders
* v4


## Release Notes

### WZDx v4.2 (February 2023)

#### New Functionality

- Add `impacted_cds_curb_zones` property to the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/objects/WorkZoneRoadEvent.md) object to allow indicating if any [Curb Data Specification](https://github.com/openmobilityfoundation/curb-data-specification) curb zones are impacted by the work zone using new [CdsCurbZonesReference](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/objects/CdsCurbZonesReference.md) object.
- Add `work_zone_type` property to the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/objects/WorkZoneRoadEvent.md) object to allow indicating if a work zone road event represents a planned moving operation, an active moving operation, or a standard static work zone. 
- Add the following values to the [Direction](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/enumerated-types/Direction.md) enumerated type:
    - `inner-loop`
    - `outer-loop`
- Add `velocity_kph` property to the [FieldDeviceCoreDetails](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/objects/FieldDeviceCoreDetails.md) object to allow indicating the velocity of a field device.
- Add `work-truck-with-lights-flashing` to the [MarkedLocationType](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/enumerated-types/MarkedLocationType.md) enumerated type.

#### Cleanup
- Expand the description of the `geometry` property on the [RoadEventFeature](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/objects/RoadEventFeature.md) object to clarify how geometry should be used to represent a road event.
- Update the description of the `open` and `closed` enumerations of the [LaneStatus](https://github.com/usdot-jpo-ode/wzdx/tree/v4.2/spec-content/enumerated-types/LaneStatus.md) enumerated type.
- Update the description of the [FlashingBeacon](/spec-content/objects/FlashingBeacon.md) object to clarify that it should only be used as a flashing warning beacon mounted on a temporary traffic control device.

### WZDx v4.1 (September 2022)

#### New Functionality
- Added `is_moving` boolean property to the [FieldDeviceCoreDetails](/spec-content/objects/FieldDeviceCoreDetails.md) to allow indicating if any field device is moving as part of a mobile operation.
- Added `road_direction` property to the [FieldDeviceCoreDetails](/spec-content/objects/FieldDeviceCoreDetails.md) to allow providing the direction of the roadway that a field device is associated with.
- Added recommendation to use Universally Unique Identifiers (UUID) for the `id` property of the [RoadEventFeature](/spec-content/objects/RoadEventFeature.md), [FieldDeviceFeature](/spec-content/objects/FieldDeviceFeature.md), and [FeedDataSource](/spec-content/objects/FeedDataSource.md), noting that a UUID may be required in the next major release.
- Added `name` property to [RoadEventCoreDetails](/spec-content/objects/RoadEventCoreDetails.md) to allow providing a human-friendly name for a road event.
- Added the following values to the [MarkedLocationType](/spec-content/enumerated-types/MarkedLocationType.md) enumerated type:
    - `personal-device`
    - `ramp-closure`
    - `road-closure`
    - `delineator`
- Added the following values to the [Direction](/spec-content/enumerated-types/Direction.md) enumerated type:
    - `undefined`
    - `unknown`
- Added `no-passing` to the [RestrictionType](/spec-content/enumerated-types/RestrictionType.md) enumerated type.
- Added `sign_text` property to the [FlashingBeacon](/spec-content/objects/FlashingBeacon.md) object.
- Added a [TrafficSignal](/spec-content/objects/TrafficSignal.md) object to allow represent temporary traffic signals in a WZDx Device Feed.
- Added `two-way-center-turn-lane` to the [LaneType](/spec-content/enumerated-types/LaneType.md) enumerated type to replace the existing `center-left-turn-lane` with a more generic value.

#### Refactoring
- Deprecated `is_moving` property on the [ArrowBoard](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/ArrowBoard.md); use the new `is_moving` on the [FieldDeviceCoreDetails](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/FieldDeviceCoreDetails.md) instead.
- Changed the conformance of the `road_event_id` property on the [TrafficSensorLaneData](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/TrafficSensorLaneData.md) from "Required" to "Optional" to allow providing lane-level data without a defined road event.
- Deprecated the `road_event_feed_info` property on the [WorkZoneFeed](/https://github.com/usdot-jpo-ode/wzdx/blob/fixes/release-v4.1/spec-content/objects/WorkZoneFeed.md) object; use the new `feed_info` property instead.
- Added `is_start_position_verified` and `is_end_position_verified` boolean properties to the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/WorkZoneRoadEvent.md) to allow indiciating if the start and end positions are verified and clarify what verified means; these properties replace `beginning_accuracy` and `ending_accuracy`.
- Deprecated the `beginning_accuracy` and `ending_accuracy` properties on the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/WorkZoneRoadEvent.md) object; use the new `is_start_position_verified` and `is_end_position_verified` properties instead.
- Added `is_start_date_verified` and `is_end_date_verified` boolean properties to the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/WorkZoneRoadEvent.md) to allow indiciating if the start and end date and times are verified and clarify what verified means; these properties replace `start_date_accuracy` and `end_date_accuracy`.
- Deprecated the `start_date_accuracy` and `end_date_accuracy` properties on the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/WorkZoneRoadEvent.md) object; use the new `is_start_date_verified` and `is_end_date_verified` properties instead.
- Deprecated the `event_status` property on the [WorkZoneRoadEvent](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/WorkZoneRoadEvent.md) object.
- Changed the conformance of the `road_names` property on the [FieldDeviceCoreDetails](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/FieldDeviceCoreDetails.md) from "Required" to "Optional".
- Deprecated the `traffic-signal` value in the [MarkedLocationType](/https://github.com/usdot-jpo-ode/wzdx/blob/fixes/release-v4.1/spec-content/enumerated-types/MarkedLocationType.md) enumerated type; use the new [TrafficSignal](/https://github.com/usdot-jpo-ode/wzdx/blob/fixes/release-v4.1/spec-content/objects/TrafficSignal.md) object instead.
- Deprecated the `center-left-turn-lane` value in the [LaneType](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/enumerated-types/LaneType.md) enumerated type; use the new `two-way-center-turn-lane` instead.
- Add a `related_road_events` property (and new supporting object [RelatedRoadEvent](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/RelatedRoadEvent.md) and enumerated type [RelatedRoadEventType](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/enumerated-types/RelatedRoadEventType.md)) to the [RoadEventCoreDetails](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/RoadEventCoreDetails.md) to allow explicitly defining relationships/connections between road events; this replaces the [Relationship](/https://github.com/usdot-jpo-ode/wzdx/blob/fixes/release-v4.1/spec-content/objects/Relationship.md) object concept.
- Deprecated the `relationship` property on the [RoadEventCoreDetails](https://github.com/usdot-jpo-ode/wzdx/blob/main/spec-content/objects/RoadEventCoreDetails.md); use the new `related_road_events` property instead.

#### Cleanup
- Changed the type of the `average_speed_kph`, `volume_vph`, and `occupancy_percent` properties on the [TrafficSensor](/spec-content/objects/TrafficSensor.md) and [TrafficSensorLaneData](/spec-content/objects/TrafficSensorLaneData.md) object from "Integer" to "Number"
- Changed the allowed minimum value for `average_speed_kph` on [TrafficSensorLaneData](/spec-content/objects/TrafficSensorLaneData.md) from `1` to `0`.
- Added a `feed_info` property to the [WorkZoneFeed](/spec-content/objects/WorkZoneFeed.md) object to replace the `road_event_feed_info`.
- Expand the description of the `update_date` property on the [RoadEventCoreDetails](/spec-content/objects/RoadEventCoreDetails.md) and [FieldDeviceCoreDetails](/spec-content/objects/FieldDeviceCoreDetails.md) to clarify what the value represents.
- Remove the [RoadRestrictionFeed](/spec-content/objects/RoadRestrictionFeed.md) (it moved to [usdot-jpo-ode/TDx](https://github.com/usdot-jpo-ode/TDx)).

## Usage 
Model objects in this package can be used independently to create a feed. However, it is highly encourged that you build your WZDx feed using the built in builder classes.

These builders ensure a valid feed output is generated.

Example: Creating a v4 workzone/detour (wzdxfeed) feed can be done using the builder classes either individually or fluently:

#### Converting to Json
The models and converters designed within make use of Newtonsoft Json serialization and it is recommended to use over the .Net Json serializer.

### Using individual builders:
``` cs
	var feedBuilder = RoadEventFeedBuilder.Factory("wsdot").Create();

	var sourceBuilder = new RoadEventSourceBuilder("wsdot-cia");
	feedBuilder = feedBuilder.WithSource(sourceBuilder);

	var featureBuilder = new WorkZoneRoadEventFeatureBuilder("wsdot-wzdb", 
        "work-zone-002E", 
        "SR-90", Direction.Eastbound);
    
    featureBuilder = featureBuilder.WithGeometry(MultiPoint.FromCoordinates(new[] { Position.From(0, 0), Position.From(10, 10) }));

	sourceBuilder = sourceBuilder.WithFeature(featureBuilder);

	RoadEventsFeed wzdx = feedBuilder.Result();
```

### Using fluent builder interface:
``` cs
    RoadEventsFeed wzdx = RoadEventFeedBuilder
        .Factory("wsdot")
        .Create()
        .WithSource("wsdot-wzdb", sourceBuilder => sourceBuilder
            .WithFeature("work-zone-001N", feature => feature
                .WorkZone("SR-5", Direction.Northbound, MultiPoint.FromCoordinates(new[] { Position.From(0, 0), Position.From(10, 10) }))
                
                /* additional feature details */
            )
            .WithFeature("work-zone-001S", feature => feature
                .WorkZone("SR-5", Direction.Southbound, MultiPoint.FromCoordinates(new[] { Position.From(0, 0), Position.From(10, 10) }))
                
                /* additional feature details */
            )
            .WithFeature("detour-001S", feature => feature
                .Detour("SR-5", Direction.Northbound, LineString.FromCoordinates(new[] { Position.From(0, 0), Position.From(10, 10) }))
                
                /* additional feature details */
            ))
        .Result();
```