#if MVC
using System;
using System.Web.Routing;

namespace FluentRouting
{
    internal static class Require
    {
        internal static void NameNotNullOrEmpty(string name)
        {
            if (name == null)
                throw Error.ArgumentNull("name");
            if (name.Length == 0)
                throw Error.ArgumentOutOfRange("name");
        }

        internal static void ControllerName(string controller)
        {
            if (controller == null)
                throw Error.ArgumentNull("controller");
            if (controller.Length == 0)
                throw Error.ArgumentOutOfRange("controller");
        }

        internal static void ActionName(string action)
        {
            if (action == null)
                throw Error.ArgumentNull("action");
            if (action.Length == 0)
                throw Error.ArgumentOutOfRange("action");
        }

        internal static void Namespace(string ns)
        {
            if (ns == null)
                throw Error.ArgumentNull("ns");
            if (ns.Length == 0)
                throw Error.ArgumentOutOfRange("ns");
        }

        internal static void AreaName(string name)
        {
            NameNotNullOrEmpty(name);
        }

        internal static void Url(string url)
        {
            if (url == null)
                throw Error.ArgumentNull("url");
        }

        internal static void Routes(RouteCollection routes)
        {
            if (routes == null)
                throw Error.ArgumentNull("routes");
        }

        internal static void Route(Route route)
        {
            if (route == null)
                throw Error.ArgumentNull("route");
        }

        internal static void Key(string key)
        {
            if (key == null)
                throw Error.ArgumentNull("key");
        }

        internal static void Value(object value)
        {
            if (value == null)
                throw Error.ArgumentNull("value");
        }

        internal static void Pattern(string pattern)
        {
            if (pattern == null)
                throw Error.ArgumentNull("pattern");
            if (pattern.Length == 0)
                throw Error.ArgumentOutOfRange("pattern");
        }

        internal static void Constraint(IRouteConstraint constraint)
        {
            if (constraint == null)
                throw Error.ArgumentNull("constraint");
        }

        internal static void AtLeastOneNamespace(string[] namespaces)
        {
            if (namespaces == null)
                throw Error.ArgumentNull("namespaces");
            if (namespaces.Length == 0)
                throw Error.ArgumentOutOfRange("namespaces");
        }

        internal static void Specification(Action<IRouteBuilder> specification)
        {
            if (specification == null)
                throw Error.ArgumentNull("specification");
        }

        internal static void Modifier(Action<Route> modifier)
        {
            if (modifier == null)
                throw Error.ArgumentNull("modifier");
        }

        internal static void Parent(RouteScope parent)
        {
            if (parent == null)
                throw Error.ArgumentNull("parent");
        }
    }
}
#endif
