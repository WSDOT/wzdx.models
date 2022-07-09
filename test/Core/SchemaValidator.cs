using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Wsdot.Wzdx.Models.Tests.Core
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
            return Load(path, new LocalUrlResolver());
        }

        public static SchemaValidator Load(Uri path, JSchemaResolver resolver)
        {
            var resource = resolver.GetSchemaResource(new ResolveSchemaContext(), new SchemaReference() { BaseUri = path });
            return Load(resource, resolver);
        }

        public static SchemaValidator Load(Stream stream, JSchemaResolver resolver)
        {
            var reader = new JsonTextReader(new StreamReader(stream));
            var schema = JSchema.Load(reader, new JSchemaReaderSettings()
            {
                Resolver = resolver
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

    internal class LocalUrlResolver : JSchemaResolver
    {
        public override Stream GetSchemaResource(ResolveSchemaContext context, SchemaReference reference)
        {
            return TryResolveLocal(context, reference, out var content)
                ? content
                : throw new ArgumentException(
                    $"No local copy of referenced url {reference.BaseUri}",
                    nameof(reference));
        }
        
        private static bool TryResolveLocal(ResolveSchemaContext context, SchemaReference reference, out Stream content)
        {
            var uri = reference.BaseUri ?? new Uri("http://localhost");
            // read from the directory
            var path = $".\\local\\{uri.Host}\\{uri.AbsolutePath}";
            content = File.Exists(path) ? File.OpenRead(path) : new MemoryStream();
            return content.Length > 0;
        }
    }
}
