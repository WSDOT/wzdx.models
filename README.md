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
* Builders
* Feeds
* Devices
* WorkZones (RoadEvents, e.g., Detours, Restrictions, WorkZones )


## Usage
While model objects can be used independently to create a feed, builders can be used to ensure a valid feed output is generated.
For example, creating a v4 workzone/detour (wzdxfeed) feed can be done using the builder classes either individually or fluently:

### Using individual builders:
``` cs
    //...
	var feedBuilder = RoadEventFeedBuilder.Factory("wsdot").Create();

	var sourceBuilder = new RoadEventSourceBuilder("wsdot-cia");
	feedBuilder.WithSource(sourceBuilder);

	var featureBuilder = new WorkZoneRoadEventFeatureBuilder("wsdit-wzdb", "work-zone-002E", "SR-90", Direction.Eastbound);
	sourceBuilder.WithFeature(featureBuilder);

	RoadEventsFeed wzdx = feedBuilder.Result();
    //...
```

### Using fluent builder interface:
``` cs
    //...
    RoadEventsFeed wzdx = RoadEventFeedBuilder
        .Factory("wsdot")
        .Create()
        .WithSource("wsdot-wzdb", sourceBuilder => sourceBuilder
            .WithWorkZoneFeature("work-zone-001N", "SR-5", Direction.Northbound, featureBuilder =>
            {
                /* additional feature details */
            })
            .WithWorkZoneFeature("work-zone-001S", "SR-5", Direction.Southbound, featureBuilder =>
            {
                /* additional feature details */
            })
            .WithDetourFeature("detour-001N", "SR-5", Direction.Northbound, featureBuilder =>
            {
                /* additional feature details */
            }))
        .Result();
    //...
```

