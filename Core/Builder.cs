using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Wsdot.Wzdx.Core
{
    public abstract class Builder<T> : IBuilder<T>
    {
        private protected readonly ICollection<Action<T>> Configuration;

        protected Builder(IEnumerable<Action<T>> configuration, Action<T> step)
        {
            Configuration = new List<Action<T>>(configuration) { step };
        }

        [Pure]
        protected abstract Func<T> ResultFactory { get; }

        [Pure]
        public virtual T Result()
        {
            var result = ResultFactory();
            foreach (var config in Configuration)
            {
                config(result);
            }

            return result;
        }
    }
}