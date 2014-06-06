#if MVC
using System;
using System.Web.Routing;

namespace SharpRouting
{
    /// <summary>
    ///   Fluent API to build ASP.NET MVC routes mapped to an action.
    /// </summary>
    public interface IActionRouteBuilder : IFluent
    {
        /// <summary>
        ///   Maps the URL to an action on the current controller.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="route">
        ///   An optional name suffix for the route.
        ///   If not specified, the default is the value of the <paramref name="name"/> parameter.
        ///   This name suffix is appended to the controller name to generate route names in the form <c>Controller.Action</c>.
        /// </param>
        /// <returns>The mapped URL route.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="name"/> is empty.
        /// </exception>
        Route IsAction(string name, string route = null);

        /// <summary>
        ///   Declares that the URL applies to HTTP GET requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Get();

        /// <summary>
        ///   Declares that the URL applies to HTTP POST requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Post();

        /// <summary>
        ///   Declares that the URL applies to HTTP PUT requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Put();

        /// <summary>
        ///   Declares that the URL applies to HTTP DELETE requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Delete();

        /// <summary>
        ///   Declares that the URL applies to HTTP HEAD requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Head();

        /// <summary>
        ///   Declares that the URL applies to HTTP PATCH requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Patch();

        /// <summary>
        ///   Declares that the URL applies to HTTP OPTIONS requests.
        /// </summary>
        /// <returns>The route builder.</returns>
        IActionRouteBuilder Options();
    }
}
#endif
