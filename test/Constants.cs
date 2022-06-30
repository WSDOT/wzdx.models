namespace Wsdot.Wzdx.Models.Tests
{
    internal static class Constants
    {
        public const string DefaultPublisher = "wzdx";
        public const string DefaultSourceId = "wzdx.planning";

        public static class Schema
        {
            public static class V4
            {
                public const string RoadEventsFeedSchema = "https://raw.githubusercontent.com/usdot-jpo-ode/wzdx/main/schemas/4.0/WZDxFeed.json";
                public const string FieldDevicesFeedSchema = "https://raw.githubusercontent.com/usdot-jpo-ode/wzdx/main/schemas/4.0/SwzDeviceFeed.json";
                public const string RoadRestrictionsEventFeedSchema = "https://raw.githubusercontent.com/usdot-jpo-ode/wzdx/main/schemas/4.0/RoadRestrictionFeed.json";

                public const string FeedInfoSchema = "https://raw.githubusercontent.com/usdot-jpo-ode/wzdx/main/schemas/4.0/FeedInfo.json";
            }
        }
    }
}
