using System;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace uAOP.Core.Helpers
{
    public static class NameHelper
    {
        public static void AppendTypeName(StringBuilder stringBuilder, Type declaringType)
        {
            stringBuilder.Append(declaringType.FullName);
            if (declaringType.IsGenericType)
            {
                var genericArguments = declaringType.GetGenericArguments();
                AppendGenericArguments(stringBuilder, genericArguments);
            }
        }

        public static void AppendGenericArguments(StringBuilder stringBuilder, Type[] genericArguments)
        {
            stringBuilder.Append('<');
            for (var i = 0; i < genericArguments.Length; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(genericArguments[i].Name);
            }
            stringBuilder.Append('>');
        }

        public static void AppendArguments(StringBuilder stringBuilder, IParameterCollection arguments)
        {
            stringBuilder.Append('(');
            for (var i = 0; i < arguments.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(arguments[i]);
            }
            stringBuilder.Append(')');
        }
    }
}
