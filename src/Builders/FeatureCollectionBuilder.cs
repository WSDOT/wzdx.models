//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Wsdot.Wzdx.Builders
//{
//    public delegate IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> FeatureCollectionBuilderSetup<TFeatureBuilderFactory, TFeatureBuilder, TFeature>
//        (IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> builder)
//        where TFeatureBuilderFactory : IFeatureBuilderFactory
//        where TFeatureBuilder : IBuilder<TFeature>
//        where TFeature : IFeature
//    ;

//    public interface IFeatureCollectionBuilder<out TFeatureBuilderFactory, in TFeatureBuilder, out TFeature> : IBuilder<IEnumerable<TFeature>>
//        where TFeatureBuilderFactory : IFeatureBuilderFactory
//        where TFeatureBuilder : IBuilder<TFeature>
//        where TFeature : IFeature
//    {
//        IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> WithFeature(string featureId, Func<TFeatureBuilderFactory, TFeatureBuilder> setup);
//    }

//    public delegate Func<TFeatureBuilderFactory, TFeatureBuilder, TFeature> FeatureBuilderFactory<in TFeatureBuilderFactory, in TFeatureBuilder, out TFeature>()
//        where TFeatureBuilderFactory : IFeatureBuilderFactory
//        where TFeatureBuilder : IBuilder<TFeature>
//        where TFeature : IFeature
//    ;

//    public abstract class FeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> :
//        IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature>
//        where TFeatureBuilderFactory : class, IFeatureBuilderFactory
//        where TFeatureBuilder : IBuilder<TFeature>
//        where TFeature : IFeature
//    {
//        private readonly ICollection<TFeatureBuilder> _builders;

//        private readonly string _sourceId;

//        public FeatureCollectionBuilder(string sourceId)
//        {
//            _sourceId = sourceId;
//            _builders = new List<TFeatureBuilder>();
//        }

//        public virtual IFeatureCollectionBuilder<TFeatureBuilderFactory, TFeatureBuilder, TFeature> WithFeature(string featureId, Func<TFeatureBuilderFactory, TFeatureBuilder> setup)
//        {
//            _builders.Add(setup(new FeatureBuilderFactory(_sourceId, featureId) as TFeatureBuilderFactory));
//            return this;
//        }

//        public IEnumerable<TFeature> Result()
//        {
//            return _builders.Select(builder => builder.Result());
//        }
//    }

//}
