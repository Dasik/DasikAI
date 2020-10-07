using System;
using System.Linq;

namespace DasikAI.Common.Utility
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFromGeneric(this Type c, Type type)
        {
            if (!c.IsGenericType)
                throw new ArgumentException("type must be generic", nameof(c));


            if (type == null || type == typeof(object))
                return false;


            if (type.IsGenericType && type.GetGenericTypeDefinition() == c)
                return true;


            return c.IsAssignableFromGeneric(type.BaseType)
                   || type.GetInterfaces().Any(c.IsAssignableFromGeneric);
        }
    }
}