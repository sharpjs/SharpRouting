#if MVC
using System;
using System.Web;
using System.Web.Routing;

// TODO: This class is needed only on MVC4 and below.

namespace SharpRouting.Constraints
{
    /// <summary>
    ///   Constrains a URL route parameter to valid <c>Guid</c> values.
    /// </summary>
    public class GuidConstraint : IRouteConstraint
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="GuidConstraint"/> class.
        /// </summary>
        protected GuidConstraint() { }

        private static readonly GuidConstraint
            _Instance = new GuidConstraint();

        /// <summary>
        ///   Gets the singleton instance of <see cref="GuidConstraint"/>.
        /// </summary>
        public static GuidConstraint Instance
        {
            get { return _Instance; }
        }

        /// <inheritdoc/>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration &&
                RouteExtensions.IgnoreConstraintsWhenGeneratingUrls)
                return true;

            Guid value;
            var candidate = values[parameterName];
            return candidate is Guid
                || Guid.TryParse(candidate as string, out value);
        }
    }
}
#endif
