#if MVC
using System;
using System.Web;
using System.Web.Routing;

// TODO: This class is needed only on MVC4 and below.

namespace FluentRouting.Constraints
{
    /// <summary>
    ///   Constrains a URL route parameter to valid <c>Int32</c> values.
    /// </summary>
    public class Int32Constraint : IRouteConstraint
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="Int32Constraint"/> class.
        /// </summary>
        protected Int32Constraint() { }

        private static readonly Int32Constraint
            _Instance = new Int32Constraint();

        /// <summary>
        ///   Gets the singleton instance of <see cref="Int32Constraint"/>.
        /// </summary>
        public static Int32Constraint Instance
        {
            get { return _Instance; }
        }

        /// <inheritdoc/>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            Int32 value;
            var candidate = values[parameterName];
            return candidate is Int32
                || Int32.TryParse(candidate as string, out value);
        }
    }
}
#endif
