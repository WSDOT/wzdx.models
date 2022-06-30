using System;

namespace Wsdot.Wzdx.Core
{
    internal class DelegatingFactory<T> : IFactory<T>
    {
        private readonly Func<T> _factory;

        public DelegatingFactory(Func<T> factory)
        {
            _factory = factory;
        }

        public T Create()
        {
            return _factory();
        }
    }
}