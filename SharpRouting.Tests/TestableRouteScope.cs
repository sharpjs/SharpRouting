using System.Web.Routing;

namespace SharpRouting
{
    internal class TestableRouteScope : RouteScope
    {
        public TestableRouteScope(RouteCollection routes, string controller, string url)
            : base(routes, controller, url) { }

        public TestableRouteScope(RouteScope parent, string controller = null, string url = null)
            : base(parent, controller, url) { }

        public new RouteCollection Routes
        {
            get { return base.Routes; }
        }

        public new RouteScope Parent
        {
            get { return base.Parent; }
        }

        public new string ControllerName
        {
            get { return base.ControllerName; }
        }

        public new string Url
        {
            get { return base.Url; }
        }

        public new void SetOptional(string key)
        {
            base.SetOptional(key);
        }

        public new void SetDefault(string key, object value)
        {
            base.SetDefault(key, value);
        }

        public new void SetPattern(string key, string pattern)
        {
            base.SetPattern(key, pattern);
        }

        public new void SetConstraint(string key, IRouteConstraint constraint)
        {
            base.SetConstraint(key, constraint);
        }

        public new void SetDataToken(string key, object value)
        {
            base.SetDataToken(key, value);
        }

        public new Route MapAction(string action, string route = null)
        {
            return base.MapAction(action, route);
        }
    }
}
