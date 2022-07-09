﻿using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using Wsdot.Wzdx.Models.Tests.Core;
using Xunit;

namespace Wsdot.Wzdx.Models.Tests
{
    public abstract class SchemaTests
    {
        // todo: cache schema in temp
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

        [Fact]
        public void OnValidateWithEmptySchemaThrowsNotSupported()
        {
            var validator = SchemaValidator.Empty();
            Assert.Throws<NotSupportedException>(() => validator.TryValidate("{}", out var _));
        }

        [Fact]
        public void NestedValidationErrorsShouldBeOutputToMessage()
        {
            const string expected = @" (0: 0)[]
	 (0: 0)[]
	 (0: 0)[]
";
            var errors = new List<ValidationError>
            {
                new ValidationError()
                {
                    ChildErrors = { new ValidationError(), new ValidationError() }
                }
            };

            Assert.Equal(expected, errors.ToMessage());
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