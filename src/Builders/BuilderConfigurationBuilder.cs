using System;
using System.Linq.Expressions;
using Wzdx.Core;

namespace Wzdx
{
    /// <summary>
    /// Provides an Immutable builder configuration builder, using a fluent interface to apply configuration changes to a builder configuration.
    /// </summary>
    public class BuilderConfigurationBuilder<T> : IBuilder<BuilderConfiguration<T>>
    {
        private BuilderConfiguration<T> Configuration { get; }

        public BuilderConfigurationBuilder(BuilderConfiguration<T> configuration)
        {
            Configuration = configuration;
        }

        public BuilderConfigurationBuilder<T> WithSet<TProperty>(Expression<Func<T, TProperty>> selector, TProperty value)
        {
            return With(configuration => configuration.Set(selector, value));
        }

        public BuilderConfigurationBuilder<T> WithSet<TProperty>(Expression<Func<T, TProperty>> selector, Action<T> setup)
        {
            return With(configuration => configuration.Set(selector, setup));
        }

        public BuilderConfigurationBuilder<T> WithDefault<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            return With(configuration => configuration.Default(selector));
        }

        public BuilderConfigurationBuilder<T> WithDefault<TProperty>(Expression<Func<T, TProperty>> selector, TProperty value)
        {
            return With(configuration => configuration.Default(selector, value));
        }

        public BuilderConfigurationBuilder<T> WithCombine<TProperty>(Expression<Func<T, TProperty>> selector, Action<T> setup)
        {
            return With(configuration => configuration.Combine(selector, setup));
        }
        
        public BuilderConfiguration<T> Result()
        {
            return Configuration.Clone();
        }

        private BuilderConfigurationBuilder<T> With(Action<BuilderConfiguration<T>> setup)
        {
            var configuration = Configuration.Clone();
            setup(configuration);
            return new BuilderConfigurationBuilder<T>(configuration);
        }
    }
}