using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Wzdx.Common
{
    internal static class ExpressionSelectorExtensions
    {
        public static PropertyInfo GetPropertyInfo<T, TProperty>(this Expression<Func<T, TProperty>> selector)
        {
            var type = typeof(T);

            if (!(selector.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{selector}' refers to a method, not a property.");

            var info = member.Member as PropertyInfo;
            if (info == null)
                throw new ArgumentException($"Expression '{selector}' refers to a field, not a property.");

            if (type != info.ReflectedType && !type.IsSubclassOf(info.ReflectedType ?? type))
                throw new ArgumentException($"Expression '{selector}' refers to a property that is not from type {type}.");

            return info;
        }

        private static string GetMemberName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)expression).Member.Name;
                case ExpressionType.Convert:
                    return GetMemberName(((UnaryExpression)expression).Operand);
                default:
                    throw new NotSupportedException(expression.NodeType.ToString());
            }
        }
    }
}
