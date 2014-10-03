#if MVC
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpRouting
{
    /// <summary>
    ///   Extends ASP.NET MVC routing, providing the SharpRouting fluent API and convenience methods.
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        ///   Gets or sets whether SharpRouting constraints are ignored during URL generation.
        ///   The default value is <c>false</c> (not ignored).
        /// </summary>
        /// <remarks>
        ///   If this property is set to <c>true</c>, URLs can be generated
        ///     (for example, by <c>Url.RouteUrl</c>) that violate the constraints for a route.
        ///   This can be useful if client-side code, such as a templating engine,
        ///     will reform the URL into a valid one.
        /// </remarks>
        public static bool IgnoreConstraintsWhenGeneratingUrls { get; set; }

        /// <summary>
        ///   Maps URL routes for a root controller, using the given specification.
        /// </summary>
        /// <param name="routes">The collection of URL routes.</param>
        /// <param name="name">The name of the controller.</param>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="routes"/>, <paramref name="name"/>, or <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="name"/> is empty.
        /// </exception>
        public static void MapController(this RouteCollection routes, string name, Action<IRouteBuilder> specification)
        {
            new RouteBuilder(routes, name).Apply(specification);
        }

        /// <summary>
        ///   Maps URL routes for a root controller, using the given specification.
        /// </summary>
        /// <typeparam name="TController">The type of controller.</typeparam>
        /// <param name="routes">The collection of URL routes.</param>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="routes"/> or <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <typeparamref name="TController"/> is not a valid controller type.
        /// </exception>
        public static void Map<TController>(this RouteCollection routes, Action<IRouteBuilder> specification)
            where TController : IController
        {
            var type = typeof(TController).RequireControllerType();
            routes.MapController(type.GetControllerName(), specification);
        }

        /// <summary>
        ///   Maps URL routes for a root controller, using the specification in the controller's <c>RegisterRoutes</c> method.
        /// </summary>
        /// <typeparam name="TController">The type of controller.</typeparam>
        /// <param name="routes">The collection of URL routes.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="routes"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <typeparamref name="TController"/> is not a valid controller type.
        /// </exception>
        /// <exception cref="MissingMethodException">
        ///   The controller does not have a <c>RegisterRoutes</c> method, or
        ///   the method is defined incorrectly.
        /// </exception>
        public static void Map<TController>(this RouteCollection routes)
            where TController : IController
        {
            var type = typeof(TController).RequireControllerType();
            routes.MapController(type.GetControllerName(), type.GetRouteSpecification());
        }

        /// <summary>
        ///   Configures a URL route with the specified default.
        ///   Any existing default with an equal key is replaced.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="key">The unique key of the default.</param>
        /// <param name="value">The value of the default.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static Route WithDefault(this Route route, string key, object value)
        {
            Require.Route(route);
            Require.Key(key);
            Require.Value(value);

            var defaults = route.Defaults;
            if (defaults == null)
                route.Defaults = defaults = new RouteValueDictionary();

            defaults[key] = value;
            return route;
        }

        /// <summary>
        ///   Configures a URL route with the specified constraint pattern.
        ///   Any existing constraint with an equal key is replaced.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="key">The unique key of the constraint.</param>
        /// <param name="pattern">The constraint pattern, a .NET regular expression.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/>, <paramref name="key"/>, or <paramref name="pattern"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="pattern"/> is empty.
        /// </exception>
        public static Route WithConstraint(this Route route, string key, string pattern)
        {
            Require.Route(route);
            Require.Key(key);
            Require.Pattern(pattern);

            return WithConstraintCore(route, key, pattern);
        }

        /// <summary>
        ///   Configures a URL route with the specified constraint object.
        ///   Any existing constraint with an equal key is replaced.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="key">The unique key of the constraint.</param>
        /// <param name="constraint">The constraint object.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/>, <paramref name="key"/>, or <paramref name="constraint"/> is <c>null</c>.
        /// </exception>
        public static Route WithConstraint(this Route route, string key, IRouteConstraint constraint)
        {
            Require.Route(route);
            Require.Key(key);
            Require.Constraint(constraint);

            return WithConstraintCore(route, key, constraint);
        }

        private static Route WithConstraintCore(Route route, string key, object value)
        {
            var constraints = route.Constraints;
            if (constraints == null)
                route.Constraints = constraints = new RouteValueDictionary();

            constraints[key] = value;
            return route;
        }

        /// <summary>
        ///   Configures a URL route with the specified data token.
        ///   Any existing data token with an equal key is replaced.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="key">The unique key of the data token.</param>
        /// <param name="value">The data token.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static Route WithDataToken(this Route route, string key, object value)
        {
            Require.Route(route);
            Require.Key  (key);

            var dataTokens = route.DataTokens;
            if (dataTokens == null)
                route.DataTokens = dataTokens = new RouteValueDictionary();

            dataTokens[key] = value;
            return route;
        }

        /// <summary>
        ///   Maps a URL route to the specified action.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="controller">The name of the controller.</param>
        /// <param name="action">The name of the action.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/>, <paramref name="controller"/>, or <paramref name="action"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   Either <paramref name="controller"/> or <paramref name="action"/> is empty.
        /// </exception>
        public static Route ToAction(this Route route, string controller, string action)
        {
            Require.ControllerName(controller);
            Require.ActionName(action);

            return route
                .WithDefault("controller", controller)
                .WithDefault("action",     action);
        }

        /// <summary>
        ///   Configures a URL route parameter as optional.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route WithOptional(this Route route, string name)
        {
            return route.WithDefault(name, UrlParameter.Optional);
        }

        /// <summary>
        ///   Configures a URL route to apply only to HTTP GET requests.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForGet(this Route route)
        {
            return route.WithConstraint("httpMethod", HttpGet);
        }

        /// <summary>
        ///   Configures a URL route to apply only to HTTP POST requests.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForPost(this Route route)
        {
            return route.WithConstraint("httpMethod", HttpPost);
        }

        /// <summary>
        ///   Configures a URL route to apply only to HTTP PUT requests.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForPut(this Route route)
        {
            return route.WithConstraint("httpMethod", HttpPut);
        }

        /// <summary>
        ///   Configures a URL route to apply only to HTTP PUT or POST requests.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForPutOrPost(this Route route)
        {
            return route.WithConstraint("httpMethod", HttpPutOrPost);
        }

        /// <summary>
        ///   Configures a URL route to apply only to HTTP DELETE requests.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForDelete(this Route route)
        {
            return route.WithConstraint("httpMethod", HttpDelete);
        }

        /// <summary>
        ///   Configures a URL route to apply only to HTTP DELETE or POST requests.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForDeleteOrPost(this Route route)
        {
            return route.WithConstraint("httpMethod", HttpDeleteOrPost);
        }

        /// <summary>
        ///   Configures a URL route to apply only to requests made via the specified HTTP verbs.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="verbs">The HTTP verbs.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="route"/> is <c>null</c>.
        /// </exception>
        public static Route ForVerbs(this Route route, HttpVerbs verbs)
        {
            var methods = new List<string>();

            foreach (var entry in HttpVerbMethods)
                if ((verbs & entry.Key) != 0)
                    methods.Add(entry.Value);

            return route.WithConstraint("httpMethod", new HttpMethodConstraint(methods.ToArray()));
        }

        /// <summary>
        ///   Configures a URL route to apply only to controllers in the specified namespaces.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="namespaces">
        ///   The list of namespaces, which must contain at least one item.
        ///   If an item ends with <c>".*"</c>, its child namespaces are included.
        /// </param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/> or <paramref name="namespaces"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="namespaces"/> is empty.
        /// </exception>
        /// <remarks>
        ///   This method disables the ASP.NET MVC <c>UseNamespaceFallback</c> behavior.
        /// </remarks>
        public static Route InNamespaces(this Route route, params string[] namespaces)
        {
            Require.AtLeastOneNamespace(namespaces);

            return route
                .WithDataToken("Namespaces", namespaces)
                .WithDataToken("UseNamespaceFallback", false);
        }

        /// <summary>
        ///   Configures the URL route to apply within the specified ASP.NET MVC area.
        /// </summary>
        /// <param name="route">The URL route.</param>
        /// <param name="name">The name of the area.</param>
        /// <returns>The URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="route"/> or <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="name"/> is empty.
        /// </exception>
        public static Route InArea(this Route route, string name)
        {
            Require.NameNotNullOrEmpty(name);
            return route.WithDataToken("area", name);
        }

        private static readonly IRouteConstraint
            HttpGet          = new HttpMethodConstraint("GET"),
            HttpPost         = new HttpMethodConstraint("POST"),
            HttpPut          = new HttpMethodConstraint("PUT"),            // Must allow POST for form-based method override
            HttpPutOrPost    = new HttpMethodConstraint("PUT", "POST"),    // Must allow POST for form-based method override
            HttpDelete       = new HttpMethodConstraint("DELETE"),         // Must allow POST for form-based method override
            HttpDeleteOrPost = new HttpMethodConstraint("DELETE", "POST"); // Must allow POST for form-based method override

        private static readonly KeyValuePair<HttpVerbs, string>[] HttpVerbMethods =
        {
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Get,     "GET"    ),
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Post,    "POST"   ),
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Put,     "PUT"    ),
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Delete,  "DELETE" ),
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Head,    "HEAD"   ),
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Patch,   "PATCH"  ),
            new KeyValuePair<HttpVerbs, string>(HttpVerbs.Options, "OPTIONS")
        };
    }
}
#endif
