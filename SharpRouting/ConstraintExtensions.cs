#if MVC
using SharpRouting.Constraints;

namespace SharpRouting
{
    /// <summary>
    ///   Extends the SharpRouting fluent API, providing common constraints for URL parameters.
    /// </summary>
    public static class ConstraintExtensions
    {
        /// <summary>
        ///   Constraints the URL parameter to valid <c>Int32</c> values.
        /// </summary>
        /// <param name="builder">The route builder.</param>
        /// <returns>The route builder.</returns>
        public static IParameterRouteBuilder Int32(this IParameterRouteBuilder builder)
        {
            return builder.WithConstraint(Int32Constraint.Instance);
        }

        /// <summary>
        ///   Constraints the URL parameter to valid <c>Guid</c> values.
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
