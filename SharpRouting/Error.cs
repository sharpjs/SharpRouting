#if MVC
using System;

namespace SharpRouting
{
    internal static class Error
    {
        internal static Exception ArgumentNull(string parameterName)
        {
            return new ArgumentNullException(parameterName);
        }

        internal static Exception ArgumentOutOfRange(string parameterName)
        {
            return new ArgumentOutOfRangeException(parameterName);
        }

        internal static Exception InvalidControllerType(Type type, string parameterName)
        {
            var message = string.Format
            (
                "Invalid controller type: {0}.  " +
                "A controller must " +
                    "be public, " +
                    "not be abstract, " +
                    "be in a namespace, " +
                    "implement IController, " +
                    "and have a name that ends with 'Controller'.",
                type.FullName
            );

            return new ArgumentOutOfRangeException(parameterName, message);
        }

        internal static Exception SpecificationNotFound(Type type)
        {
            var message = string.Format
            (
                "Could not find a route specification method in {0}.  " +
                "The method should be declared as: public static void RegisterRoutes(IRouteBuilder r) {{ ... }}",
                type.FullName
            );

            return new MissingMethodException(message);
        }

        internal static Exception LeadingOrTrailingSlashInUrl(string url)
        {
            return new FormatException("URLs are relative.  Leading or trailing slashes are not supported.");
        }
    }
}
#endif
