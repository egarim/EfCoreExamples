using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EfCoreExamples.SoftDelete
{
    public static class TypeExtensions
    {
        public static string GetFullNameWithAssemblyName(this Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
        {
            //Check.NotNull(type, nameof(type));

            return type.IsAssignableTo(typeof(TTarget));
        }
    }
}
