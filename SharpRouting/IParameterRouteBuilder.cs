#if MVC
using System;
using System.Web.Routing;

namespace SharpRouting
{
    /// <summary>
    ///   Fluent API to build ASP.NET MVC routes for a specific URL parameter.
    /// </summary>
    public interface IParameterRouteBuilder : IUrlRouteBuilder
    {
        /// <summary>
        ///   Declares the URL parameter as optional.
        /// </summary>
        /// <returns>The route builder.</returns>
        IParameterRouteBuilder Optional();

        /// <summary>
        ///   Provides a default value for the URL parameter.
        /// </summary>
        /// <param name="value">The default value.</param>
        /// <returns>The route builder.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="value"/> is <c>null</c>.
        /// </exception>
        IParameterRouteBuilder WithDefault(object value);

        /// <summary>
        ///   Applies a pattern constraint to the URL parameter.
        /// </summary>
        /// <param name="pattern">A .NET regular expression pattern.</param>
        /// <returns>The route builder.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="pattern"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="pattern"/> is empty.
        /// </exception>
        IParameterRouteBuilder WithPattern(string pattern);

        /// <summary>
        ///   Applies a custom constraint to the URL parameter.
        /// </summary>
        /// <param name="constraint">The custom constraint.</param>
        /// <returns>The route builder.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="constraint"/> is <c>null</c>.
        /// </exception>
        IParameterRouteBuilder WithConstraint(IRouteConstraint constraint);
    }
}
#endif
