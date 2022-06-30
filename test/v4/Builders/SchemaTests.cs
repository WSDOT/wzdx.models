using System;
using Newtonsoft.Json;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests.v4.Builders
{
    public abstract class SchemaTests
    {
        private SchemaValidator Validator { get; }
        protected SchemaTests(Uri schemaUri)
        {
            Validator = SchemaValidator.Load(schemaUri);
        }
        
        [Fact]
        public void ShouldFail()
        {
            EnsureInvalid(new object());
        }

        protected void EnsureValid(object value)
        {
            Assert.True(IsValid(value, out var message), message);
        }

        protected void EnsureInvalid(object value)
        {
            Assert.False(IsValid(value, out var message), message);
        }

        protected bool IsValid<T>(T value, out string message)
        {
            var result = false;
            try
            {
                var json = JsonConvert.SerializeObject(value);
                result = Validator.TryValidate(json, out var errors);
                message = errors.ToMessage();
            }
            catch (JsonSerializationException e)
            {
                message = e.Message;
            }
            return result;
        }
    }
}