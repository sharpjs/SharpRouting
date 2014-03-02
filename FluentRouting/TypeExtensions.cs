#if MVC
using System;
using System.Reflection;
using System.Web.Mvc;

namespace FluentRouting
{
    internal static class TypeExtensions
    {
        internal static bool IsController(this Type type)
        {
            return type != null
                && type.IsPublic
                && type.IsAbstract == false
                && type.Name.EndsWith(ControllerNameSuffix, StringComparison.OrdinalIgnoreCase)
                && type.Namespace != null
                && typeof(IController).IsAssignableFrom(type);
        }

        internal static Type RequireControllerType(this Type type)
        {
            if (type == null)
                throw Error.ArgumentNull("type");
            if (!type.IsController())
                throw Error.InvalidControllerType(type, "type");

            return type;
        }

        internal static string GetControllerName(this Type type)
        {
            RequireControllerType(type);

            var name = type.Name;
            return name.Substring(0, name.Length - ControllerNameSuffix.Length);
        }

        internal static Action<IRouteBuilder> GetRouteSpecification(this Type controllerType)
        {
            var method = controllerType.GetMethod
                (SpecificationName, SpecificationBindingFlags, null, SpecificationParameterTypes, null);

            if (method == null || method.ReturnType != typeof(void))
                throw Error.SpecificationNotFound(controllerType);

            return (Action<IRouteBuilder>) Delegate.CreateDelegate(typeof(Action<IRouteBuilder>), method);
        }

        private const string
            ControllerNameSuffix = "Controller",
            SpecificationName    = "RegisterRoutes";

        private const BindingFlags
            SpecificationBindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        private static readonly Type[]
            SpecificationParameterTypes = { typeof(RouteBuilder) };
    }
}
#endif
