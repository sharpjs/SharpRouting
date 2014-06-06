#if MVC
using System;
using System.Web.Routing;

namespace SharpRouting
{
    internal class RouteBuilder : RouteScope, IRouteBuilder
    {
        internal RouteBuilder(RouteCollection routes, string controller)
            : base(routes, controller, "") { }

        internal RouteBuilder(RouteScope parent, string controller = null, string url = null)
            : base(parent, controller, url) { }

        public new IUrlRouteBuilder Url(string url)
        {
            return new UrlRouteBuilder(this, url);
        }

        public IParameterRouteBuilder Parameter(string name)
        {
            return new ParameterRouteBuilder(this, name);
        }

        internal void Apply(Action<IRouteBuilder> specification)
        {
            Require.Specification(specification);
            specification(this);
        }
    }
}
#endif
