using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Wzdx.Common;

namespace Wzdx
{
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

        public void Set<TProperty>(Expression<Func<T, TProperty>> selector, TProperty value)
        {
            var propertyInfo = selector.GetPropertyInfo();
            Set(selector.GetPropertyInfo(), source => propertyInfo.SetValue(source, value));
        }

        public void Set<TProperty>(Expression<Func<T, TProperty>> selector, Action<T> setup)
        {
            Set(selector.GetPropertyInfo(), setup);
        }

        public void Default<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            Default(selector, default(TProperty));
        }

        public void Default<TProperty>(Expression<Func<T, TProperty>> selector, TProperty value)
        {
            Set(selector, value);
        }

        public void ApplyTo(T source)
        {
            foreach (var value in _configuration.Values)
            {
                value(source);
            }
        }

        public void Combine<TProperty>(Expression<Func<T, TProperty>> selector, Action<T> setup)
        {
            var propertyInfo = selector.GetPropertyInfo();
            _configuration.AddOrUpdate(propertyInfo, setup, (info, action) => (Action<T>)Delegate.Combine(action, setup));
        }

        public BuilderConfiguration<T> Clone()
        {
            return new BuilderConfiguration<T>(_configuration);
        }

        private void Set(PropertyInfo propertyInfo, Action<T> setup)
        {
            _configuration.AddOrUpdate(propertyInfo, setup, (info, action) => setup);
        }
    }
}