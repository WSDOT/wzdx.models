using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Wzdx.Common;

namespace Wzdx
{
    /// <summary>
    /// Generic class builder configuration. 
    /// </summary>
    /// <remarks>Contains configuration changes to be applied to an object being built</remarks>
    /// <typeparam name="T">Generic type being built</typeparam>
    public class BuilderConfiguration<T>
    {
        private readonly ConcurrentDictionary<PropertyInfo, Action<T>> _configuration;

        public BuilderConfiguration() : 
            this(new Dictionary<PropertyInfo, Action<T>>())
        {

        }

        private BuilderConfiguration(IDictionary<PropertyInfo, Action<T>> configuration)
        {
            _configuration = new ConcurrentDictionary<PropertyInfo, Action<T>>(configuration);
        }

        /// <summary>
        /// Set property value
        /// </summary>
        /// <typeparam name="TProperty">Property selector type</typeparam>
        /// <param name="selector">Property selector</param>
        /// <param name="value">Value to set property to</param>
        public void Set<TProperty>(Expression<Func<T, TProperty>> selector, TProperty value)
        {
            var propertyInfo = selector.GetPropertyInfo();
            Set(selector.GetPropertyInfo(), source => propertyInfo.SetValue(source, value));
        }

        /// <summary>
        /// Set property value through delegate
        /// </summary>
        /// <typeparam name="TProperty">Property selector type</typeparam>
        /// <param name="selector">Property selector</param>
        /// <param name="setup">Action delegate to take on property value</param>
        public void Set<TProperty>(Expression<Func<T, TProperty>> selector, Action<T> setup)
        {
            Set(selector.GetPropertyInfo(), setup);
        }

        /// <summary>
        /// Set property to default type value
        /// </summary>
        /// <typeparam name="TProperty">Property selector type</typeparam>
        /// <param name="selector">Property selector</param>
        public void Default<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            Default(selector, default(TProperty));
        }

        /// <summary>
        /// Set property default value
        /// </summary>
        /// <typeparam name="TProperty">Property selector type</typeparam>
        /// <param name="selector">Property selector</param>
        /// <param name="value">Value to set property to</param>
        public void Default<TProperty>(Expression<Func<T, TProperty>> selector, TProperty value)
        {
            Set(selector, value);
        }

        /// <summary>
        /// Apply builder configuration to object
        /// </summary>
        /// <param name="source">Object to be configured</param>
        public void ApplyTo(T source)
        {
            foreach (var value in _configuration.Values)
            {
                value(source);
            }
        }

        /// <summary>
        /// Combine prior delegate configurations 
        /// </summary>
        /// <typeparam name="TProperty">Property selector type</typeparam>
        /// <param name="selector">Property selector</param>
        /// <param name="setup">Action delegate to take on property value</param>
        public void Combine<TProperty>(Expression<Func<T, TProperty>> selector, Action<T> setup)
        {
            var propertyInfo = selector.GetPropertyInfo();
            _configuration.AddOrUpdate(propertyInfo, setup, (info, action) => (Action<T>)Delegate.Combine(action, setup));
        }

        /// <summary>
        /// Create clone of builder configuration
        /// </summary>
        /// <returns></returns>
        public BuilderConfiguration<T> Clone()
        {
            return new BuilderConfiguration<T>(_configuration);
        }

        /// <summary>
        /// Internal set of configuration
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="setup"></param>
        private void Set(PropertyInfo propertyInfo, Action<T> setup)
        {
            _configuration.AddOrUpdate(propertyInfo, setup, (info, action) => setup);
        }
    }
}