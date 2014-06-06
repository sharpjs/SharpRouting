#if MVC
using System;
using System.Web.Routing;

namespace SharpRouting
{
    /// <summary>
    ///   Fluent API to build ASP.NET MVC routes for a controller.
    /// </summary>
    public interface IRouteBuilder : IRelativeRouteBuilder
    {
        /// <summary>
        ///   Registers an action that can modify URL routes created subsequently in the current scope and in child scopes.
        /// </summary>
        /// <param name="modifier">An action that modifies a URL route.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="modifier"/> is <c>null</c>.
        /// </exception>
        void Configure(Action<Route> modifier);
    }
}
#endif
