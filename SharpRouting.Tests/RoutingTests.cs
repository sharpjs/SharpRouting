using System.Web.Routing;
using NUnit.Framework;

namespace SharpRouting
{
    public abstract class RoutingTests
    {
        protected RouteCollection Routes { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            Routes = new RouteCollection();
        }

        // TODO: Move this to RouteExtensionTests, and then it doesn't need to subclass this class.
        internal static Route Route(
            string url,
            string controller  = null,
            string action      = null,
            object defaults    = null,
            object constraints = null,
            object dataTokens  = null)
        {
            var route = new Route
            (
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new RouteValueDictionary(dataTokens),
                routeHandler: null
            );

            if (controller != null)
                route.Defaults.Add("controller", controller);
            if (action != null)
                route.Defaults.Add("action", action);

            return route;
        }
    }
}
