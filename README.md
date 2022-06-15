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