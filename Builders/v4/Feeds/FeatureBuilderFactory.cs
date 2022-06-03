namespace Wsdot.Wzdx.v4.Feeds
{
    public interface IFeatureBuilderFactory
    {
        string FeatureId { get; }
        string SourceId { get; }
    }

    public interface IFieldDeviceFeatureBuilderFactory : IFeatureBuilderFactory { }

    public interface IRoadEventFeatureBuilderFactory : IFeatureBuilderFactory { }

    public interface IRoadRestrictionFeatureBuilderFactory : IFeatureBuilderFactory { }

    public class FeatureBuilderFactory :
        IFieldDeviceFeatureBuilderFactory,  
        IRoadEventFeatureBuilderFactory,
        IRoadRestrictionFeatureBuilderFactory
    {

        public string FeatureId { get; }
        public string SourceId { get; }

        public FeatureBuilderFactory(string sourceId, string featureId)
        {
            SourceId = sourceId;
            FeatureId = featureId;
        }
    }
}