using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Wzdx.Models.Tests
{
    internal sealed class SchemaValidator
    {
        private Func<string, IEnumerable<ValidationError>> Validator { get; }

        private SchemaValidator(JSchema schema) :
            this(value => DefaultValidator(schema, value))
        {
            // 
        }

        private SchemaValidator(Func<string, IEnumerable<ValidationError>> validator)
        {
            Validator = validator;
        }
        
        public bool TryValidate(string json, out IEnumerable<ValidationError> errors)
        {
            errors = Validate(json);
            return !errors.Any();
        }
        
        public static SchemaValidator Load(Uri path)
        {
            var client = new HttpClient();
            var request = client.GetAsync(path);
            request.Wait();

            return (request.IsCompletedSuccessfully)
                ? Load(request.Result.Content.ReadAsStream())
                : Empty();
        }

        private IEnumerable<ValidationError> Validate(string json) => Validator(json);

        // ReSharper disable once MemberCanBePrivate.Global
        public static SchemaValidator Empty() => new SchemaValidator(_ => throw new NotSupportedException("Empty schema cannot be used to validate with."));

        private static SchemaValidator Load(Stream stream)
        {
            var reader = new JsonTextReader(new StreamReader(stream));
            var schema = JSchema.Load(reader, new JSchemaReaderSettings()
            {
                Resolver = new JSchemaUrlResolver()
            });

            return new SchemaValidator(schema);
        }

        private static IEnumerable<ValidationError> DefaultValidator(JSchema schema, string json)
        {
            var obj = JToken.Parse(json);
            obj.IsValid(schema, out IList<ValidationError> errors);
            return errors;
        }
    }
}
