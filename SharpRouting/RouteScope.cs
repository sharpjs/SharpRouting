#if MVC
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpRouting
{
    /// <summary>
    ///   Provides a scope for mapping URL routes that share
    ///   a common controller name, base URL, and configuration.
    /// </summary>
    [DebuggerDisplay("Url = {Url}, Controller = {ControllerName}")]
    internal abstract class RouteScope
    {
        private readonly RouteCollection routes;
        private readonly RouteScope      parent;
        private readonly string          controller;
        private readonly string          url;

        private List<Action<Route>>      modifiers;
        private HttpVerbs                verbs;

        protected RouteScope(RouteCollection routes, string controller, string url)
        {
            Require.Routes(routes);
            Require.ControllerName(controller);
            Require.Url(url);

            this.routes     = routes;
            this.controller = controller;
            this.url        = ValidateUrl(url);
        }

        protected RouteScope(RouteScope parent, string controller = null, string url = null)
        {
            Require.Parent(parent);
            if (controller == "")
                throw Error.ArgumentOutOfRange("controller");
            // Allow null controller name

            this.parent     = parent;
            this.routes     = parent.routes;
            this.controller = controller ?? parent.controller;
            this.url        = CombineUrl(parent.url, url == null ? "" : ValidateUrl(url));
        }

        protected RouteCollection Routes
        {
            get { return routes; }
        }

        protected RouteScope Parent
        {
            get { return parent; }
        }

        protected string ControllerName
        {
            get { return controller; }
        }

        protected string Url
        {
            get { return url; }
        }

        protected void SetOptional(string key)
        {
            Require.Key(key);
            Configure(r => r.WithOptional(key));
        }

        protected void SetDefault(string key, object value)
        {
            Require.Key(key);
            Require.Value(value);
            Configure(r => r.WithDefault(key, value));
        }

        protected void SetPattern(string key, string pattern)
        {
            Require.Key(key);
            Require.Pattern(pattern);
            Configure(r => r.WithConstraint(key, pattern));
        }

        protected void SetConstraint(string key, IRouteConstraint constraint)
        {
            Require.Key(key);
            Require.Constraint(constraint);
            Configure(r => r.WithConstraint(key, constraint));
        }

        protected void SetDataToken(string key, object value)
        {
            Require.Key(key);
            Configure(r => r.WithDataToken(key, value));
        }

        public void Configure(Action<Route> modifier)
        {
            Require.Modifier(modifier);

            var modifiers = this.modifiers;
            if (modifiers == null)
                modifiers = this.modifiers = new List<Action<Route>>();

            modifiers.Add(modifier);
        }

        protected void AddHttpVerbs(HttpVerbs verbs)
        {
            this.verbs |= verbs;
        }

        protected Route MapAction(string action, string route = null)
        {
            Require.ActionName(action);

            route = string.Concat(controller, ".", route ?? action);

            var result = routes.MapRoute(route, url).ToAction(controller, action);
            ApplyModifiers(result);
            ApplyHttpVerbs(result);
            return result;
        }

        private void ApplyModifiers(Route route)
        {
            var parent = this.parent;
            if (parent != null)
                parent.ApplyModifiers(route);

            var modifiers = this.modifiers;
            if (modifiers != null)
                modifiers.ForEach(a => a(route));
        }

        private void ApplyHttpVerbs(Route route)
        {
            var verbs = this.verbs;
            if (verbs == default(HttpVerbs))
                return;

            route.ForVerbs(verbs);
        }

        private static string ValidateUrl(string url)
        {
            if (url.StartsWith("/") || url.EndsWith("/"))
                throw Error.LeadingOrTrailingSlashInUrl(url);

            return url;
        }

        private static string CombineUrl(string a, string b)
        {
            if (a.Length == 0)
                return b;
            if (b.Length == 0)
                return a;

            return string.Concat(a, "/", b);
        }
    }
}
#endif
