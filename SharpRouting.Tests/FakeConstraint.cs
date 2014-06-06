using System.Web;
using System.Web.Routing;

namespace SharpRouting
{
    internal class FakeConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContextBase      httpContext,
            Route                route,
            string               parameterName,
            RouteValueDictionary values,
            RouteDirection       routeDirection)
        {
            return true;
        }
    }
}
