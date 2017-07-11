using System;
using System.Text;
using System.Linq.Expressions;

namespace ExtensionTool
{
    public static class MemberInfoGetting
    {
        // to get variable name
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }

    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }
    }


    public static class StringBuilderExtensions
    {
        public static void Clear(this StringBuilder builder)
        {
            builder.Length = 0;
        }
    }
    
}



