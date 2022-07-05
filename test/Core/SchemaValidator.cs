using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Wsdot.Wzdx.Models.Tests.Core
{
    internal sealed class SchemaValidator
    {
        private static readonly IDictionary<Uri, SchemaValidator> KnownValidators = new ConcurrentDictionary<Uri, SchemaValidator>();

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
            SchemaValidator validator;
            lock (KnownValidators)
            {
                if (KnownValidators.TryGetValue(path, out validator))
                    return validator;

                var client = new HttpClient(new CacheMessageHandler());
                var request = client.GetAsync(path);
                request.Wait();

                if (request.IsCompletedSuccessfully)
                {
                    validator = Load(request.Result.Content.ReadAsStream());
                    KnownValidators.TryAdd(path, validator);
                }
                else
                {
                    validator = Empty();
                }
            }

            return validator;
        }

        public static bool TryLoad(Uri uri, out SchemaValidator validator)
        {
            validator = Load(uri);
            return !validator.IsEmpty;
        }

        public static SchemaValidator Load(Stream stream)
        {
            var reader = new JsonTextReader(new StreamReader(stream));
            var schema = JSchema.Load(reader, new JSchemaReaderSettings()
            {
                Resolver = new JSchemaUrlResolver()
            });

            return new SchemaValidator(schema);
        }

        private IEnumerable<ValidationError> Validate(string json) => Validator(json);

        // ReSharper disable once MemberCanBePrivate.Global

        public static SchemaValidator Empty() => new SchemaValidator(_ => throw new NotSupportedException("Empty schema cannot be used to validate with.")) { IsEmpty = true };

        public bool IsEmpty { get; private init; }

        private static IEnumerable<ValidationError> DefaultValidator(JSchema schema, string json)
        {
            var obj = JToken.Parse(json);
            obj.IsValid(schema, out IList<ValidationError> errors);
            return errors;
        }
    }
}
