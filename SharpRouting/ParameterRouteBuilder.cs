#if MVC
using System.Web.Routing;

namespace SharpRouting
{
    internal class ParameterRouteBuilder : UrlRouteBuilder, IParameterRouteBuilder
    {
        private readonly string name;

        internal ParameterRouteBuilder(RouteScope parent, string name)
            : base(parent, FormatParameter(name))
        {
            this.name = name;
        }

        public IParameterRouteBuilder Optional()
        {
            SetOptional(name);
            return this;
        }

        public IParameterRouteBuilder WithDefault(object value)
        {
            SetDefault(name, value);
            return this;
        }

        public IParameterRouteBuilder WithPattern(string pattern)
        {
            SetPattern(name, pattern);
            return this;
        }

        public IParameterRouteBuilder WithConstraint(IRouteConstraint constraint)
        {
            SetConstraint(name, constraint);
            return this;
        }

        private static string FormatParameter(string name)
        {
            Require.NameNotNullOrEmpty(name);
            return string.Concat("{", name, "}");
        }
    }
}
#endif
