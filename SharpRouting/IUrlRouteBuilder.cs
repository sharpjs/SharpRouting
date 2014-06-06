#if MVC
using System;
using System.Web.Mvc;

namespace SharpRouting
{
    /// <summary>
    ///   Fluent API to build ASP.NET MVC routes for a specific URL.
    /// </summary>
    public interface IUrlRouteBuilder : IRelativeRouteBuilder, IActionRouteBuilder
    {
        /// <summary>
        ///   Maps the URL to multiple actions on the current controller, using the given specification.
        /// </summary>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        void Is(Action<IRouteBuilder> specification);

        /// <summary>
        ///   Maps the URL to a controller, using the specification in the controller's <c>RegisterRoutes</c> method.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <typeparamref name="TController"/> is not a valid controller type.
        /// </exception>
        void IsController<TController>()
            where TController : IController;

        /// <summary>
        ///   Maps the URL to a controller, using the given specification.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <typeparamref name="TController"/> is not a valid controller type.
        /// </exception>
        void IsController<TController>(Action<IRouteBuilder> specification)
            where TController : IController;

        /// <summary>
        ///   Maps the URL to a controller, using the given specification.
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="name"/> or <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="name"/> is empty.
        /// </exception>
        void IsController(string name, Action<IRouteBuilder> specification);

        /// <summary>
        ///   Maps the URL to an area, using the specification in the default controller's <c>RegisterRoutes</c> method.
        /// </summary>
        /// <typeparam name="TController">The type of the default controller for the area.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <typeparamref name="TController"/> is not a valid controller type.
        /// </exception>
        void IsArea<TController>()
            where TController : IController;

        /// <summary>
        ///   Maps the URL to an area, using the given specification.
        /// </summary>
        /// <typeparam name="TController">The type of the default controller for the area.</typeparam>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <typeparamref name="TController"/> is not a valid controller type.
        /// </exception>
        void IsArea<TController>(Action<IRouteBuilder> specification)
            where TController : IController;

        /// <summary>
        ///   Maps the URL to an area, using the given specification.
        /// </summary>
        /// <param name="name">The name of the area and its default controller.</param>
        /// <param name="ns">The .NET namespace that contains the area's controllers and views.</param>
        /// <param name="specification">An action that specifies routes via the SharpRouting fluent API.</param>
        /// <exception cref="ArgumentNullException">
        ///   Either <paramref name="name"/>, <paramref name="ns"/>, or <paramref name="specification"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   Either <paramref name="name"/> or <paramref name="ns"/> is empty.
        /// </exception>
        void IsArea(string name, string ns, Action<IRouteBuilder> specification);
    }
}
#endif
