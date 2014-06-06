#if MVC
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpRouting
{
    internal class UrlRouteBuilder : RouteBuilder, IUrlRouteBuilder
    {
        internal UrlRouteBuilder(RouteScope parent, string url)
            : base(parent, url: RequireUrl(url)) { }

        public IActionRouteBuilder Get()
        {
            AddHttpVerbs(HttpVerbs.Get);
            return this;
        }

        public IActionRouteBuilder Post()
        {
            AddHttpVerbs(HttpVerbs.Post);
            return this;
        }

        public IActionRouteBuilder Put()
        {
            AddHttpVerbs(HttpVerbs.Put);
            return this;
        }

        public IActionRouteBuilder Delete()
        {
            AddHttpVerbs(HttpVerbs.Delete);
            return this;
        }

        public IActionRouteBuilder Head()
        {
            AddHttpVerbs(HttpVerbs.Head);
            return this;
        }

        public IActionRouteBuilder Patch()
        {
            AddHttpVerbs(HttpVerbs.Patch);
            return this;
        }

        public IActionRouteBuilder Options()
        {
            AddHttpVerbs(HttpVerbs.Options);
            return this;
        }

        public Route IsAction(string name, string route = null)
        {
            return MapAction(name, route);
        }

        public void Is(Action<IRouteBuilder> specification)
        {
            new RouteBuilder(this).Apply(specification);
        }

        public void IsController<TController>()
            where TController : IController
        {
            var type = typeof(TController).RequireControllerType();
            IsController(type.GetControllerName(), type.GetRouteSpecification());
        }

        public void IsController<TController>(Action<IRouteBuilder> specification)
            where TController : IController
        {
            var type = typeof(TController).RequireControllerType();
            IsController(type.GetControllerName(), specification);
        }

        public void IsController(string name, Action<IRouteBuilder> specification)
        {
            if (name == null)
                throw Error.ArgumentNull("name");
            new RouteBuilder(this, controller: name).Apply(specification);
        }

        public void IsArea<TController>()
            where TController : IController
        {
            var type = typeof(TController).RequireControllerType();
            IsArea(type.GetControllerName(), type.Namespace, type.GetRouteSpecification());
        }

        public void IsArea<TController>(Action<IRouteBuilder> specification)
            where TController : IController
        {
            var type = typeof(TController).RequireControllerType();
            IsArea(type.GetControllerName(), type.Namespace, specification);
        }

        public void IsArea(string name, string ns, Action<IRouteBuilder> specification)
        {
            Require.AreaName(name); // TODO: Don't need to test for empty here; that is handled by RouteBuilder ctor
            Require.Namespace(ns);
            var builder = new RouteBuilder(this, controller: name);
            builder.Configure(r => r.InArea(name).InNamespaces(ns + ".*"));
            builder.Apply(specification);
        }

        private static string RequireUrl(string url)
        {
            Require.Url(url);
            return url;
        }
    }
}
#endif
