using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Schema;

namespace Wzdx.Models.Tests.Core
{
    public static class SchemaValidationErrorExtensions
    {
        public static string ToMessage(this IEnumerable<ValidationError> errors)
        {
            return errors.WithChildren(new StringBuilder(), 0).ToString();
        }

        private static StringBuilder WithChildren(this IEnumerable<ValidationError> errors, StringBuilder builder, int depth)
        {
            var indent = string.Join("", Enumerable.Range(0, depth).Select(_ => "\t"));
            foreach (var error in errors)
            {
                builder.AppendLine($"{indent}{error.Message} ({error.LineNumber}: {error.LinePosition})[{error.Path}]");
                if (error.ChildErrors.Any())
                    builder = error.ChildErrors.WithChildren(builder, depth + 1);
            }

            return builder;
        }
    }
}