using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Wzdx.Core;
using Wzdx.GeoJson;

namespace Wzdx.v4.Feeds
{
    public abstract class FeedSourceFeatureBuilder<T, TFeature, TFeatureBuilder> :
        FeedSourceBuilder<T>
        where T : FeedSourceFeatureBuilder<T, TFeature, TFeatureBuilder>
        where TFeature : IFeature
        where TFeatureBuilder : IBuilder<TFeature>
    {
        private readonly ICollection<IBuilder<TFeature>> _featureBuilders;
        
        protected FeedSourceFeatureBuilder(string sourceId) : base(sourceId)
        {
            _featureBuilders = new List<IBuilder<TFeature>>();
        }

        public T WithFeature(IBuilder<TFeature> builder)
        {
            _featureBuilders.Add(builder);
            return Derived();
        }

        [Pure]
        public IEnumerable<TFeature> Features()
        {
            return _featureBuilders.Select(builder => builder.Result());
        }

        [Pure]
        public FeedDataSource Result(out IEnumerable<TFeature> features)
        {
            var result = new FeedDataSource();
            Configuration.ApplyTo(result);
            features = Features();
            return result;
        }
    }
}