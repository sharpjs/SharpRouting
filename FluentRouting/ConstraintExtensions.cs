#if MVC
using FluentRouting.Constraints;

namespace FluentRouting
{
    /// <summary>
    ///   Extends the Fluent Routing API, providing common constraints for URL parameters.
    /// </summary>
    public static class ConstraintExtensions
    {
        /// <summary>
        ///   Constraints the URL parameter to valid GUIDs.
        /// </summary>
        /// <param name="builder">The route builder.</param>
        /// <returns>The route builder.</returns>
        public static IParameterRouteBuilder Guid(this IParameterRouteBuilder builder)
        {
            return builder.WithConstraint(GuidConstraint.Instance);
        }
    }
}
#endif
