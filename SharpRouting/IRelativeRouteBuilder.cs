#if MVC
using System;

namespace SharpRouting
{
    /// <summary>
    ///   Fluent API to build ASP.NET MVC routes relative to a base path.
    /// </summary>
    public interface IRelativeRouteBuilder : IFluent
    {
        /// <summary>
        ///   Begins mapping for a URL.
        /// </summary>
        /// <param name="url">The URL, relative to the current scope.</param>
        /// <returns>A builder object exposing a mapping API for the URL.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="url"/> is <c>null</c>.
        /// </exception>
        IUrlRouteBuilder Url(string url);

        /// <summary>
        ///   Begins mapping for a URL parameter.
        /// </summary>
        /// <param name="name">The name of the URL parameter, relative to the current scope.</param>
        /// <returns>A builder object exposing a mapping API for the URL parameter.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="name"/> is empty.
        /// </exception>
        IParameterRouteBuilder Parameter(string name);
    }
}
#endif
